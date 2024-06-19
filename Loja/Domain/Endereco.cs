using Loja.Domain.Errors;

namespace Loja.Domain;

/// <summary>
/// Entidade que representa um Endereço
/// </summary>
public class Endereco: Entity<Endereco>
{
    public string Logradouro { get; private set; } = null!;

    public string  Numero { get; private set; } = null!;

    public string Complemento { get; private set; } = null!;

    public string Bairro { get; private set; } = null!;

    public CEP Cep { get; private set; } = null!;

    public virtual UF UF { get; private set; } = null!; // Lazy loading

    public Guid? ClienteId { get; private set; }

    public virtual Cliente Cliente { get; private set; } = null!;

    public Guid? PedidoId { get; private set; }

    public virtual Pedido Pedido { get; private set; } = null!;

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    protected Endereco() { }

    /// <summary>
    /// Método fábrica que valida e cria um endereço
    /// </summary>
    /// <param name="logradouro">Logradouro</param>
    /// <param name="numero">Número</param>
    /// <param name="complemento">Complemento</param>
    /// <param name="bairro">Bairro</param>
    /// <param name="cep">CEP</param>
    /// <param name="uf">UF</param>
    /// <returns></returns>
    public static Result<Endereco> Create(string logradouro, string numero, string complemento, string bairro, CEP cep, UF uf)
    {
        List<ErroEntidade> erros = valida(logradouro, numero, complemento, bairro, cep, uf);

        return erros.Count == 0 ? new Endereco()
        {
            Id = Guid.NewGuid(),
            Logradouro = logradouro,
            Numero = numero,
            Complemento = complemento,
            Bairro = bairro,
            Cep = cep,
            UF = uf
        } :
        erros;
    }

    /// <summary>
    /// Cria uma cópia do endereço
    /// </summary>
    /// <returns>Cópia do endereço</returns>
    public Endereco Copy()
    {
        return new Endereco()
        {
            Id = Guid.NewGuid(),
            Logradouro = this.Logradouro,
            Numero = this.Numero,
            Bairro = this.Bairro,
            Complemento = this.Complemento,
            Cep = this.Cep,
            UF = this.UF
        };
    }

    /// <summary>
    /// Valida os atributos do endereço
    /// </summary>
    /// <param name="logradouro">Logradouro: não pode ser branco</param>
    /// <param name="numero">Número: não pode ser nulo</param>
    /// <param name="complemento">Complemento: não pode ser nulo</param>
    /// <param name="bairro">Bairro: não pode ser branco</param>
    /// <param name="cep">CEP: não pode ser nulo</param>
    /// <param name="uf">UF: não pode ser nulo</param>
    /// <returns>Lista de erros de validação</returns>
    private static List<ErroEntidade> valida(string logradouro, string numero, string complemento, string bairro, CEP cep, UF uf)
    {
        List<ErroEntidade> erros = [];

        if (string.IsNullOrWhiteSpace(logradouro))
            erros.Add(ErroEntidade.ENDERECO_LOGRADOURO_INVALIDO);

        if (numero is null)
            erros.Add(ErroEntidade.ENDERECO_NUMERO_INVALIDO);

        if (complemento is null)
            erros.Add(ErroEntidade.ENDERECO_COMPLEMENTO_INVALIDO);

        if (string.IsNullOrWhiteSpace(bairro))
            erros.Add(ErroEntidade.ENDERECO_BAIRRO_INVALIDO);

        if (cep is null)
            erros.Add(ErroEntidade.ENDERECO_CEP_INVALIDO);

        if (uf is null)
            erros.Add(ErroEntidade.ENDERECO_UF_INVALIDA);

        return erros;
    }

    /// <summary>
    /// Verifica se dois endreços são iguais
    /// </summary>
    /// <param name="outro">Outro endereço </param>
    /// <returns></returns>
    public override bool Equals(Endereco? outro) => 
        outro is Endereco endereco && 
        Logradouro == outro.Logradouro && 
        Numero == outro.Numero && 
        Complemento == outro.Complemento && 
        Bairro == outro.Bairro && 
        UF == outro.UF;
}
