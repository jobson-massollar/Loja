using Microsoft.EntityFrameworkCore;
using Loja.Domain;
using Loja.Db;

/*
 * Implemetação com lazy loading.
 * Pode fazer o eager loading onde for conveniente.
 * Todas as coleções devem ser públicas (sem encapsulamento).
 */

// Use essa opção para definir que parte do programa vai ser executada
const int opcao = 2;

var db = Sessao.dbContext;

Cliente c1, c2;
Produto p1, p2, p3;
Endereco e1, e2;
UF uf;
Preferencia pr1, pr2, pr3, pr4;

// Cadastro dos dados básicos

if (opcao == 1)
{
    pr1 = Preferencia.Create("Futebol").Value!;
    pr2 = Preferencia.Create("Basquete").Value!;
    pr3 = Preferencia.Create("Volei").Value!;
    pr4 = Preferencia.Create("Natação").Value!;

    db.Preferencias.Add(pr1);
    db.Preferencias.Add(pr2);
    db.Preferencias.Add(pr3);
    db.Preferencias.Add(pr4);

    uf = UF.Create("RJ", "Rio de Janeiro").Value!;

    c1 = new ClienteBuilder(22237067031, 
                            "Jose da Silva", 
                            "jose.silva@gmail.com", 
                            "Rua das Flores", 
                            "100", 
                            "ap 101", 
                            "Tijuca", 
                            20000000, 
                            uf).Builder().Value!;

    c2 = new ClienteBuilder()
        .ComCPF(36718168050)
        .ComNome("Maria Isabel")
        .ComEmail("maria.isabel@gmail.com")
        .ComLogradouro("Rua das Margaridas")
        .ComNumero("200")
        .ComComplemento("ap 202")
        .ComBairro("Maracanã")
        .ComCEP(21000000)
        .ComTelefone(21, 987654321)
        .ComUF(uf)
        .Builder().Value!;

    p1 = new ProdutoBuilder()
        .ComCodigoBarras("1234567890123")
        .ComDescricao("Produto 1")
        .ComPreco("R$", 100)
        .Build().Value!;

    p2 = new ProdutoBuilder()
        .ComCodigoBarras("2345678901234")
        .ComDescricao("Produto 2")
        .ComPreco("R$", 200)
        .Build().Value!;

    p3 = new ProdutoBuilder("3456789012345",
                            "Produto 3",
                            "R$", 
                            300)
                            .Build().Value!;

    db.Clientes.Add(c1);
    db.Clientes.Add(c2);

    db.Produtos.Add(p1);
    db.Produtos.Add(p2);
    db.Produtos.Add(p3);

    db.SaveChanges();
}

// Criação de Preferências dos Clientes e Pedidos

if (opcao == 2)
{
    uf = Sessao.dbContext.UFs.FirstOrDefault(uf => uf.Sigla == "RJ")!;

    c1 = db.Clientes.FirstOrDefault(c => c.Email == "jose.silva@gmail.com")!;
    c2 = db.Clientes.FirstOrDefault(c => c.CPF.Valor == 36718168050)!;
    p1 = db.Produtos.FirstOrDefault(p => p.CodigoBarras.Valor == "1234567890123")!;
    p2 = db.Produtos.FirstOrDefault(p => p.CodigoBarras.Valor == "2345678901234")!;
    p3 = db.Produtos.FirstOrDefault(p => p.CodigoBarras.Valor == "3456789012345")!;

    pr1 = db.Preferencias.FirstOrDefault(p => p.Descricao == "Futebol");
    pr2 = db.Preferencias.FirstOrDefault(p => p.Descricao == "Basquete");
    pr3 = db.Preferencias.FirstOrDefault(p => p.Descricao == "Volei");
    pr4 = db.Preferencias.FirstOrDefault(p => p.Descricao == "Natação");

    if (c1 is not null && p1 is not null && p2 is not null && pr1 is not null && pr2 is not null)
    {
        // Associa duas preferências
        c1.AddPreferencia(pr1);
        c1.AddPreferencia(pr2);

        // Cria um pedido
        var o1 = new PedidoBuilder(c1, 
                                   "Rua Barao de Mesquita", 
                                   "300", 
                                   "ap 501", 
                                   "Tijuca", 
                                   20000000, 
                                   uf).Build().Value!;
        o1.AddItem(p1, 2);
        o1.AddItem(p2, 3);

        db.Pedidos.Add(o1);

        db.SaveChanges();
    }

    if (c2 is not null && p2 is not null && p3 is not null && pr3 is not null && pr4 is not null)
    {
        // Associa duas preferências
        c2.AddPreferencia(pr3);
        c2.AddPreferencia(pr4);

        // Pedido vai ser entregue no mesmo endereço do cliente
        e2 = c2.Endereco.Copy();
        var o2 = new PedidoBuilder(c2, e2).Build().Value!;

        o2.AddItem(p2, 3);
        o2.AddItem(p3, 4);

        db.Pedidos.Add(o2);

        db.SaveChanges();
    }
}

// Consulta dos Pedidos, Cliente e Itens do Pedido

if (opcao == 3)
{
    db.Pedidos
        .Include(pedido => pedido.Itens)
        .ThenInclude(item => item.Produto)
        .Include(pedido => pedido.Cliente)
        .ThenInclude(cliente => cliente.Endereco)
        .ThenInclude(endereco => endereco.UF)
        .Include(pedido => pedido.Cliente)
        .ThenInclude(cliente => cliente.Preferencias)
        .Include(pedido => pedido.EnderecoEntrega)
        .ThenInclude(endereco => endereco.UF)
        .ToList().ForEach(pedido =>
        {
            var cliente = pedido.Cliente;

            Console.WriteLine($"Pedido: {pedido.Id}");
            Console.WriteLine("Cliente:");
            Console.WriteLine($"    Nome: {cliente.Nome}");
            Console.WriteLine($"    CPF: {cliente.CPF.Valor}");
            Console.WriteLine($"    Email: {cliente.Email}");
            Console.WriteLine($"    Endereco: {cliente.Endereco.Logradouro} {cliente.Endereco.Numero} {cliente.Endereco.Complemento} {cliente.Endereco.Bairro} {cliente.Endereco.Cep.Valor} - {cliente.Endereco.UF.Sigla}");
            Console.WriteLine($"    Telefone: ({cliente.Telefone.DDD ?? 0}) {cliente.Telefone.Numero ?? 0}");
            Console.WriteLine($"    Preferências: ({cliente.Preferencias.Count}):");
            cliente.Preferencias.ToList().ForEach(pref => Console.WriteLine($"      - {pref.Descricao}"));
            Console.WriteLine($"Itens ({pedido.Itens.Count}):");
            pedido.Itens.ToList().ForEach(it => Console.WriteLine($"- {it.Quantidade} {it.Preco.Moeda} {it.Preco.Valor} {it.Valor} {it.Produto.Descricao}"));
        });
}

// Consulta dos Clientes, Preferências e Pedidos
if (opcao == 4)
{
    db.Clientes
        .Include(c => c.Endereco)
        .ThenInclude(endereco => endereco.UF)
        .Include(c => c.Pedidos)
        .ThenInclude(p => p.Itens)
        .Include(c => c.Pedidos)
        .ThenInclude(p => p.EnderecoEntrega)
        .ThenInclude(endereco => endereco.UF)
        .Include(c => c.Preferencias)
        .ToList().ForEach(cliente =>
        {
            Console.WriteLine($"Cliente: {cliente.Id}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"CPF: {cliente.CPF.Valor}");
            Console.WriteLine($"Email: {cliente.Email}");
            Console.WriteLine($"Endereco: {cliente.Endereco.Logradouro} {cliente.Endereco.Numero} {cliente.Endereco.Complemento} {cliente.Endereco.Bairro} {cliente.Endereco.Cep.Valor} - {cliente.Endereco.UF.Sigla}");
            Console.WriteLine($"Telefone: ({cliente.Telefone.DDD ?? 0}) {cliente.Telefone.Numero ?? 0}");
            Console.WriteLine($"Preferências: ({cliente.Preferencias.Count}):");
            cliente.Preferencias.ToList().ForEach(pref => Console.WriteLine($"  - {pref.Descricao}"));
            Console.WriteLine($"Pedidos ({cliente.Pedidos.Count}):");
            cliente.Pedidos.ToList().ForEach(pedido =>
            {
                Console.WriteLine($"   Valor: ${pedido.Total}");
                Console.WriteLine($"   Entrega: {pedido.EnderecoEntrega.Logradouro} {pedido.EnderecoEntrega.Numero} {pedido.EnderecoEntrega.Complemento} {pedido.EnderecoEntrega.Bairro} {pedido.EnderecoEntrega.UF.Sigla}");
            });
        });
}
