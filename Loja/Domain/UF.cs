using System.Text.RegularExpressions;
using Loja.Domain.Errors;

namespace Loja.Domain;


/// <summary>
/// Entidade querepresenta uma unidade da Federação
/// </summary>
public class UF : Entity<UF>
{
    public string Sigla { get; private set; } = null!;

    public string Nome { get; private set; } = null!;

    /// <summary>
    /// Esse construtor deveria ser privado, mas é público por conta do EF
    /// </summary>
    public UF() { }

    /// <summary>
    /// Método fábrica que valida e cria uma UF
    /// </summary>
    /// <param name="sigla">Sigla da UF</param>
    /// <param name="nome">Nome da UF</param>
    /// <returns></returns>
    public static Result<UF> Create(string sigla, string nome)
    {
        List<ErroEntidade> erros = valida(sigla, nome);

        return erros.Count == 0 ? new UF
        {
            Sigla = sigla,
            Nome = nome
        } :
        erros;
    }

    /// <summary>
    /// Valida os atributos da UF
    /// </summary>
    /// <param name="sigla">Sigla da UF: duas letras em caixa alta</param>
    /// <param name="nome">Nome da UF:não pode estar em branco</param>
    /// <returns></returns>
    private static List<ErroEntidade> valida(string sigla, string nome)
    {
        List<ErroEntidade> erros = [];

        if (sigla is null || ! Regex.IsMatch(sigla, @"^[A-Z]{2}$"))
            erros.Add(ErroEntidade.UF_SIGLA_INVALIDA);

        if (string.IsNullOrWhiteSpace(nome))
            erros.Add(ErroEntidade.UF_NOME_INVALIDO);

        return erros;
    }

    /// <summary>
    /// Verifica se duas UFs são iguais
    /// </summary>
    /// <param name="outro">Outra UF a ser comparada</param>
    /// <returns>True, seas UFs são iguais; ou False, caso contrário</returns>
    public override bool Equals(UF? outro) => outro is not null && Sigla == outro.Sigla;
}
