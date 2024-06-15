using Loja.Domain.Errors;

namespace Loja.Domain;

public class ProdutoBuilder
{
    private string? numeroCodigoBarras;
    private string? descricao;
    private string? moeda;
    private float? valor;

    public ProdutoBuilder() { }

    public ProdutoBuilder(string numeroCodigoBarras, string descricao, string moeda, float valor)
    {
        this.numeroCodigoBarras = numeroCodigoBarras;
        this.descricao = descricao;
        this.moeda = moeda;
        this.valor = valor;
    }

    public ProdutoBuilder ComCodigoBarras(string numeroCodigoBarras)
    {
        this.numeroCodigoBarras = numeroCodigoBarras;
        return this;
    }

    public ProdutoBuilder ComDescricao(string descricao)
    {
        this.descricao = descricao;
        return this;
    }

    public ProdutoBuilder ComMoeda(string moeda)
    {
        this.moeda = moeda;
        return this;
    }

    public ProdutoBuilder ComValor(float valor)
    {
        this.valor = valor;
        return this;
    }

    public ProdutoBuilder ComPreco(string moeda, float valor)
    {
        this.moeda = moeda;
        this.valor = valor;
        return this;
    }

    public Result<Produto> Build()
    {
        // Constroi os value objects
        var codigoBarras = CodigoBarras.Create(numeroCodigoBarras);
        var preco = valor is float v ? Dinheiro.Create(moeda, v) : null;

        // Constroi o produto
        var resultProduto = Produto.Create(codigoBarras, descricao, preco);

        return resultProduto.IsSuccess ? resultProduto.Value! : resultProduto.Errors!;
    }
}
