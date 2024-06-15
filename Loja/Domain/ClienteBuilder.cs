using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Builder para a classe cliente e suas referências
/// </summary>
public class ClienteBuilder
{
    private long? numeroCPF;
    private string? nome;
    private string? email;
    private int? ddd;
    private int? numeroTel;

    /// <summary>
    /// O ClienteBuilder vai receber os dados do endereço e repassá-los para o EnderecoBuilder
    /// </summary>
    private EnderecoBuilder enderecoBuilder = null!;

    /// <summary>
    /// O ClienteBuilder também pode receber o endereço já pronto
    /// </summary>
    private Endereco? endereco;

    /// <summary>
    /// Construtor que permite definir os atributos do cliente de forma individual
    /// </summary>
    public ClienteBuilder()
    {
        enderecoBuilder = new EnderecoBuilder();
    }

    /// <summary>
    /// Construtor que os atributos detalhados e obrigatórios do cliente
    /// </summary>
    /// <param name="numeroCPF">Número do CPF</param>
    /// <param name="nome">Nome</param>
    /// <param name="email">E-mail</param>
    /// <param name="logradouro">Logradouro</param>
    /// <param name="numero">Numero</param>
    /// <param name="complemento">Complemento</param>
    /// <param name="bairro">Bairro</param>
    /// <param name="numeroCEP">Número do CEP</param>
    /// <param name="uf">UF (esse objeto vem 'pronto' porque ele é recuperado do repositório)</param>
    public ClienteBuilder(long numeroCPF,
                          string nome,
                          string email,
                          string logradouro,
                          string numero,
                          string complemento,
                          string bairro,
                          int numeroCEP,
                          UF uf)
    {
        this.numeroCPF = numeroCPF;
        this.nome = nome;
        this.email = email;
        enderecoBuilder = new EnderecoBuilder(logradouro, numero, complemento, bairro, numeroCEP, uf);
    }

    /// <summary>
    /// COnstrutor que recebe os dados obrigatórios do cliente e o objeto endereço já pronto
    /// </summary>
    /// <param name="numeroCPF">Número do CPF</param>
    /// <param name="nome">Nome</param>
    /// <param name="email">E-mail</param>
    /// <param name="endereco">Endereço do cliente</param>
    public ClienteBuilder(long numeroCPF,
                          string nome,
                          string email,
                          Endereco endereco)
    {
        this.numeroCPF = numeroCPF;
        this.nome = nome;
        this.email = email;
        this.endereco = endereco;
    }

    public ClienteBuilder ComCPF(long numeroCPF)
    {
        this.numeroCPF = numeroCPF;
        return this;
    }

    public ClienteBuilder ComNome(string nome)
    {
        this.nome = nome;
        return this;
    }

    public ClienteBuilder ComEmail(string email)
    {
        this.email = email;
        return this;
    }

    public ClienteBuilder ComLogradouro(string logradouro)
    {
        enderecoBuilder.ComLogradouro(logradouro);
        return this;
    }

    public ClienteBuilder ComNumero(string numero)
    {
        enderecoBuilder.ComNumero(numero);
        return this;
    }

    public ClienteBuilder ComComplemento(string complemento)
    {
        enderecoBuilder.ComComplemento(complemento);
        return this;
    }

    public ClienteBuilder ComBairro(string bairro)
    {
        enderecoBuilder.ComBairro(bairro);
        return this;
    }

    public ClienteBuilder ComCEP(int cep)
    {
        enderecoBuilder.ComCEP(cep);
        return this;
    }

    /// <summary>
    /// Define a UF do endereço. Diferentemente dos demais, esse objeto já vem construído porque ele
    /// deve ser gerado fora do builder.
    /// </summary>
    /// <param name="uf">UF do endereço</param>
    /// <returns>ClienteBuilder para poder implementar a fluent API</returns>
    public ClienteBuilder ComUF(UF uf)
    {
        enderecoBuilder.ComUF(uf);
        return this;
    }

    public ClienteBuilder ComTelefone(int? ddd, int? numeroTel)
    {
        this.ddd = ddd;
        this.numeroTel = numeroTel;
        return this;
    }

    public Result<Cliente> Builder()
    {
        List<ErroEntidade> erros = [];

        // Constroi os value objects
        var cpf = numeroCPF is long _cpf ? CPF.of(_cpf) : null;
        var telefone = Telefone.Create(ddd, numeroTel);

        // Endereço:
        // Se o builder recebeu o endereço pronto, então usa esse endereço.
        // Senão constroi um endereço com os dados.
        if (endereco is null)
        {
            var resultEndereco = enderecoBuilder.Build();

            if (resultEndereco.IsSuccess)
                endereco = resultEndereco.Value!;
            else
                erros.Concat(resultEndereco.Errors!);
        }

        // Constroi o cliente
        var resultCliente = Cliente.Create(cpf, nome, email, endereco, telefone);

        if (resultCliente.hasErrors)
        {
            erros.Concat(resultCliente.Errors!);

            return erros;
        }
        else
            return resultCliente.Value!;
    }
}
