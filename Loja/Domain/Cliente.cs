using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade querepresenta um cliente
/// </summary>
public class Cliente : Entity<Cliente>
{
    public CPF CPF { get; private set; } = null!;

    public string Nome { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public Telefone Telefone { get; set; } = null!;

    public virtual Endereco Endereco { get; set; } = null!; // Lazy loading

    public virtual List<Preferencia> Preferencias { get; private set; } = null!; // Lazy loading

    public virtual List<Pedido> Pedidos { get; private set; } = null!; // Lazy loading

    /// <summary>
    /// Esse construtor deveria ser privado, mas é público por conta do EF
    /// </summary>
    public Cliente() { }

    /// <summary>
    /// Método fábrica que valida e cria o cliente
    /// </summary>
    /// <param name="cpf">CPF</param>
    /// <param name="nome">Nome</param>
    /// <param name="email">E-mail</param>
    /// <param name="endereco">Endereço</param>
    /// <param name="telefone">Telefone</param>
    /// <returns></returns>
    public static Result<Cliente> Create(CPF cpf, string nome, string email, Endereco endereco, Telefone telefone)
    {
        List<ErroEntidade> erros = valida(cpf, nome, email, endereco, telefone);

        return erros.Count == 0 ? new Cliente()
        {
            Id = Guid.NewGuid(),
            CPF = cpf,
            Nome = nome,
            Email = email,
            Endereco = endereco,
            Telefone = telefone
        } :
        erros;
    }

    /// <summary>
    /// Valida os atributos do cliente
    /// </summary>
    /// <param name="cpf">CPF: não pode ser nulo</param>
    /// <param name="nome">Nome: não pode estar em branco</param>
    /// <param name="email">:E-mail: não pode estar em branco</param>
    /// <param name="endereco">Endereço: não pode ser nulo</param>
    /// <param name="telefone">: Telefone: não pode ser nulo</param>
    /// <returns>Lista de erros de validação</returns>
    private static List<ErroEntidade> valida(CPF cpf, string nome, string email, Endereco endereco, Telefone telefone)
    {
        List<ErroEntidade> erros = [];

        if (cpf is null)
            erros.Add(ErroEntidade.CLIENTE_CPF_INVALIDO);

        if (string.IsNullOrWhiteSpace(nome))
            erros.Add(ErroEntidade.CLIENTE_NOME_INVALIDO);

        if (string.IsNullOrWhiteSpace(email))
            erros.Add(ErroEntidade.CLIENTE_EMAIL_INVALIDO);

        if (endereco is null)
            erros.Add(ErroEntidade.CLIENTE_ENDERECO_INVALIDO);

        if (telefone is null)
            erros.Add(ErroEntidade.CLIENTE_TELEFONE_INVALIDO);

        return erros;
    }

    /// <summary>
    /// Adiciona uma preferência na lista de preferências do cliente
    /// </summary>
    /// <param name="pref">Preferência a ser adicionada</param>
    /// <returns>True se a preferência foi adicionada, ou False caso contrário</returns>
    public bool AddPreferencia(Preferencia pref)
    {
        if (!Preferencias.Contains(pref))
        {
            Preferencias.Add(pref);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Remove uma preferência da lista de preferências do cliente
    /// </summary>
    /// <param name="pref">Preferência a ser removida</param>
    public void RemovePreferencia(Preferencia pref) => Preferencias.Remove(pref);

    /// <summary>
    /// Verifica se dois clientes são iguais
    /// </summary>
    /// <param name="outro">Outro cliente a ser comparado</param>
    /// <returns>True, se os clientes são iguais; ou False, caso contrário</returns>
    public override bool Equals(Cliente? outro) => outro is not null && CPF == outro.CPF;
}
