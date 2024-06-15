using Loja.Domain.Errors;

namespace Loja.Domain;

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

    public PedidoBuilder()
    {
        enderecoBuilder = new EnderecoBuilder();
    }

    public PedidoBuilder(Cliente cliente, string logradouro, string numero, string complemento, string bairro, int numeroCEP, UF uf)
    {
        this.cliente = cliente;
        enderecoBuilder = new EnderecoBuilder(logradouro, numero, complemento, bairro, numeroCEP, uf);
    }

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
            erros.Concat(resultPedido.Errors);

            return erros;
        }
        else
            return resultPedido.Value!;
    }
}
