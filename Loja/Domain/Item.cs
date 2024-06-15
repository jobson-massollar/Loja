using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade que representa um item do pedido
/// </summary>
public class Item : Entity<Item>
{
    public int Quantidade { get; init; } = 0;

    public Dinheiro Preco { get; init; } = null!;

    public virtual Pedido Pedido { get; init; } = null!; // Lazy loading

    public virtual Produto Produto { get; init; } = null!; // Lazy loading

    public float Valor => Quantidade * Preco.Valor;

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    protected Item() { }

    /// <summary>
    /// Método fábrica que valida e cria um item do pedido
    /// </summary>
    /// <param name="pedido">Pedido</param>
    /// <param name="produto">Produto vendido no item</param>
    /// <param name="quantidade">Quantidade vendida no item</param>
    /// <param name="preco">Preço praticado na venda</param>
    /// <returns></returns>
    public static Result<Item> Create(Pedido pedido, Produto produto, int quantidade, Dinheiro preco)
    {
        List<ErroEntidade> erros = valida(pedido, produto, quantidade, preco);

        return erros.Count == 0 ? new Item()
        {
            Id = Guid.NewGuid(),
            Pedido = pedido,
            Produto = produto,
            Quantidade = quantidade,
            Preco = preco
        } : 
        erros;
    }

    /// <summary>
    /// Valida os atributos do item
    /// </summary>
    /// <param name="pedido">Pedido:não pode ser nulo</param>
    /// <param name="produto">Produto: não pode ser nulo</param>
    /// <param name="quantidade">QUantidade: maior que zero</param>
    /// <param name="preco">Preço: não pode ser nulo</param>
    /// <returns>Lista de erros de validação</returns>
    private static List<ErroEntidade> valida(Pedido pedido, Produto produto, int quantidade, Dinheiro preco)
    {
        List<ErroEntidade> erros = [];

        if (pedido is null)
            erros.Add(ErroEntidade.ITEM_PEDIDO_INVALIDO);

        if (quantidade <= 0)
            erros.Add(ErroEntidade.ITEM_QUANTIDADE_INVALIDA);

        if (produto is null)
            erros.Add(ErroEntidade.ITEM_PRODUTO_INVALIDO);

        if (preco is null)
            erros.Add(ErroEntidade.ITEM_PRECO_INVALIDO);

        return erros;
    }

    /// <summary>
    /// Verifica se dois itens são iguais
    /// </summary>
    /// <param name="outro">Outro item a ser comparado</param>
    /// <returns>True, se os itens são iguais; ou False, caso contrário</returns>
    public override bool Equals(Item? outro) => outro is not null && Id == outro.Id;
}
