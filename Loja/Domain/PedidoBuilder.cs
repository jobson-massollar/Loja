using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Builder para o Pedido
/// </summary>
public class PedidoBuilder
{
    private Cliente? cliente;

    /// <summary>
    /// O ClienteBuilder vai receber os dados do endereço e repassá-los para o EnderecoBuilder
    /// </summary>
    private EnderecoBuilder enderecoBuilder = null!;

    /// <summary>
    /// O ClienteBuilder também pode receber o endereço já pronto
    /// </summary>
    private Endereco? endereco;

    /// <summary>
    /// Construtor que permite definir os dados do pedido de forma individual
    /// </summary>
    public PedidoBuilder()
    {
        enderecoBuilder = new EnderecoBuilder();
    }

    /// <summary>
    /// Construtor que recebe os dados obrigatórios do pedido
    /// </summary>
    /// <param name="cliente">Cliente que fez o pedido</param>
    /// <param name="logradouro">Logradouro de entrega</param>
    /// <param name="numero">Número</param>
    /// <param name="complemento">Complemento</param>
    /// <param name="bairro">Bairro</param>
    /// <param name="numeroCEP">CEP</param>
    /// <param name="uf">UF (esse objeto vem 'pronto' porque ele é recuperado do repositório)</param>
    public PedidoBuilder(Cliente cliente, string logradouro, string numero, string complemento, string bairro, int numeroCEP, UF uf)
    {
        this.cliente = cliente;
        enderecoBuilder = new EnderecoBuilder(logradouro, numero, complemento, bairro, numeroCEP, uf);
    }

    /// <summary>
    /// Construtor que recebe os dados obrigatórios do pedido e o objeto endereço já pronto
    /// </summary>
    /// <param name="cliente">Cliente que fez o pedido</param>
    /// <param name="endereco">Endereço de entrega</param>
    public PedidoBuilder(Cliente cliente, Endereco endereco)
    {
        this.cliente = cliente;
        this.endereco = endereco;
    }

    public PedidoBuilder ComCliente(Cliente cliente)
    {
        this.cliente = cliente;
        return this;
    }

    public PedidoBuilder ComLogradouro(string logradouro)
    {
        enderecoBuilder.ComLogradouro(logradouro);
        return this;
    }

    public PedidoBuilder ComNumero(string numero)
    {
        enderecoBuilder.ComNumero(numero);
        return this;
    }

    public PedidoBuilder ComComplemento(string complemento)
    {
        enderecoBuilder.ComComplemento(complemento);
        return this;
    }

    public PedidoBuilder ComBairro(string bairro)
    {
        enderecoBuilder.ComBairro(bairro);
        return this;
    }

    public PedidoBuilder ComCEP(int cep)
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
    public PedidoBuilder ComUF(UF uf)
    {
        enderecoBuilder.ComUF(uf);
        return this;
    }

    /// <summary>
    /// Cria o pedido com os dados definidos
    /// </summary>
    /// <returns>Pedido ou lista de erros</returns>
    public Result<Pedido> Build()
    {
        List<ErroEntidade> erros = [];

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

        var resultPedido = Pedido.Create(cliente, endereco);

        if (resultPedido.hasErrors)
        {
            // Se ocorreu o erro e o endereço é nulo, então os erros sobre o endereço já estão na lista
            // Remove o erro criado pelo cliente
            if (endereco is null)
                resultPedido.Errors!.Remove(ErroEntidade.CLIENTE_ENDERECO_INVALIDO);

            erros.Concat(resultPedido.Errors);

            return erros;
        }

        return erros.Count == 0 ? resultPedido.Value! : erros;
    }
}
