using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade que representa um pedido
/// </summary>
public class Pedido : Entity<Pedido>
{
    public virtual Cliente Cliente { get; private set; } = null!; // Lazy loading

    public virtual Endereco EnderecoEntrega { get; private set; } = null!; // Lazy loading

    public virtual List<Item> Itens { get;  private set; } = new(); // Lazy loading

    public float Total => Itens.Sum(i => i.Valor);

    /// <summary>
    /// Esse construtor deveria ser privado, mas é público por conta do EF
    /// </summary>
    public Pedido() { }

    /// <summary>
    /// Método fábrica que valida e cria um pedido
    /// </summary>
    /// <param name="cliente">Cliente que compra o pedido</param>
    /// <param name="endereco">Endereço de entrega do pedido</param>
    /// <returns></returns>
    public static Result<Pedido> Create(Cliente cliente, Endereco endereco)
    {
        List<ErroEntidade> erros = valida(cliente, endereco);

        return erros.Count == 0 ? new Pedido
        {
            Id = Guid.NewGuid(),
            Cliente = cliente,
            EnderecoEntrega = endereco
        } :
        erros;
    }

    /// <summary>
    /// Adiciona um item na lista de itens do pedido
    /// </summary>
    /// <param name="produto">Produto comercializado no item</param>
    /// <param name="quantidade">Quantidade vendida no item</param>
    /// <param name="preco">Preço praticado na venda</param>
    /// <returns>True se o item foi adicionado, ou False caso contrário</returns>
    public bool AddItem(Produto produto, int quantidade, Dinheiro preco)
    {
        var result = Item.Create(this, produto, quantidade, preco);

        if (result.IsSuccess)
        {
            Itens.Add(result.Value!);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adiciona um item na lista de itens do pedido. O preço praticado é o mesmo preço de venda do produto.
    /// </summary>
    /// <param name="produto">Produto comercializado no item</param>
    /// <param name="quantidade">Quantidad evendida no item</param>
    /// <returns>True se o item foi adicionado, ou False caso contrário</returns>
    public bool AddItem(Produto produto, int quantidade) => AddItem(produto, quantidade, produto.Preco);

    private static List<ErroEntidade> valida(Cliente cliente, Endereco endereco)
    {
        List<ErroEntidade> erros = [];

        if (cliente is null)
            erros.Add(ErroEntidade.PEDIDO_CLIENTE_INVALIDO);

        if (endereco is null)
            erros.Add(ErroEntidade.PEDIDO_ENDERECO_INVALIDO);

        return erros;
    }

    /// <summary>
    /// Verifica se dois pedidos são iguais
    /// </summary>
    /// <param name="outro">Outro pedido a ser comparado</param>
    /// <returns>True, se os pedidos são iguais; ou False, caso contrário</returns>
    public override bool Equals(Pedido? outro) => outro is not null && Id == outro.Id;
}
