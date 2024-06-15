using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade que representa uma preferência
/// </summary>
public class Preferencia: Entity<Preferencia>
{
    public string Descricao { get; init; } = null!;

    public virtual List<Cliente> Clientes { get; private set; } = null!;

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    protected Preferencia() { }

    /// <summary>
    /// Método fábrica que valida e cria uma preferência
    /// </summary>
    /// <param name="descricao"></param>
    /// <returns></returns>
    public static Result<Preferencia> Create(string descricao)
    {
        List<ErroEntidade> erros = valida(descricao);

        return erros.Count == 0 ? new Preferencia { Descricao = descricao } : erros;
    }

    /// <summary>
    /// Valida os atributos da preferência
    /// </summary>
    /// <param name="descricao">Descrição da preferência: não pode ser branco</param>
    /// <returns>Lista de erros de validação</returns>
    private static List<ErroEntidade> valida(string descricao)
    {
        List<ErroEntidade> erros = [];

        if (string.IsNullOrWhiteSpace(descricao))
            erros.Add(ErroEntidade.PREFERENCIA_DESCRICAO_INVALIDA);

        return erros;
    }

    /// <summary>
    /// Verifica se duas preferências são iguais
    /// </summary>
    /// <param name="outro">Outra preferência a ser comparada</param>
    /// <returns>True, se as preferências são iguais; ou False, caso contrário</returns>
    public override bool Equals(Preferencia? outro) => outro is Preferencia pref && Descricao == pref.Descricao;
}
