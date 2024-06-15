namespace Loja.Domain;

/// <summary>
/// Value object que representa um valor monetário
/// </summary>
public record Dinheiro
{
    public string Moeda { get; private set; } = null!;

    public float Valor { get; private set; }

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    /// <param name="moeda">Símbolo da moeda</param>
    /// <param name="valor">Valor monetário</param>
    protected Dinheiro(string moeda, float valor)
    {
        Moeda = moeda;
        Valor = valor;
    }

    /// <summary>
    /// Método fábrica que valida e cria o Dinheiro
    /// </summary>
    /// <param name="moeda">Símbolo da moeda</param>
    /// <param name="valor">Valor monetário</param>
    /// <returns>DInheiro ou null, se parâmetros são inválidos</returns>
    public static Dinheiro? Create(string moeda, float valor) => 
        ! string.IsNullOrWhiteSpace(moeda) && valor >= 0 ? new Dinheiro(moeda, valor) : null;
}
