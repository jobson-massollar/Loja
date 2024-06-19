using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade que representa um produto
/// </summary>
public class Produto : Entity<Produto>
{
    public CodigoBarras CodigoBarras { get; init; } = null!;

    public string Descricao { get; private set; } = string.Empty;

    public Dinheiro Preco { get; private set; } = null!;

    public string Status { get; private set; } = string.Empty;

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    protected Produto() { }

    /// <summary>
    /// Método fábrica que valida e cria um produto.
    /// </summary>
    /// <param name="codigoBarras">Código de barras do produto</param>
    /// <param name="descricao">Descrição do produto</param>
    /// <param name="preco">Preço do produto</param>
    /// <returns>Produto ou Lista de erros</returns>
    public static Result<Produto> Create(CodigoBarras codigoBarras, string descricao, Dinheiro preco)
    {
        List<ErroEntidade> erros = valida(codigoBarras, descricao, preco);

        return erros.Count == 0 ? new Produto
        {
            Id = Guid.NewGuid(),
            CodigoBarras = codigoBarras,
            Descricao = descricao,
            Preco = preco
        } :
        erros;
    }

    /// <summary>
    /// Valida os atributos do produto
    /// </summary>
    /// <param name="codigoBarras">Código de barras:não pode ser nulo</param>
    /// <param name="descricao">Descrição: não pode ser nulo ou branco</param>
    /// <param name="preco">Preço: não pode ser nulo</param>
    /// <returns>Lista de erros de validação</returns>
    private static List<ErroEntidade> valida(CodigoBarras codigoBarras, string descricao, Dinheiro preco)
    {
        List<ErroEntidade> erros = [];

        if (codigoBarras is null)
            erros.Add(ErroEntidade.PRODUTO_CODIGO_BARRAS_INVALIDO);

        if (string.IsNullOrWhiteSpace(descricao))
            erros.Add(ErroEntidade.PRODUTO_DESCRICAO_INVALIDA);

        if (preco is null)
            erros.Add(ErroEntidade.PRODUTO_PRECO_INVALIDO);

        return erros;
    }

    /// <summary>
    /// Verifica se dois produtos são iguais
    /// </summary>
    /// <param name="outro">Outro produto a ser comparado</param>
    /// <returns>True, se os produtos são iguais; ou False, caso contrário</returns>
    public override bool Equals(Produto? outro) => 
        outro is not null && 
        CodigoBarras.Valor == outro.CodigoBarras.Valor;
}
