namespace Loja.Domain;

/// <summary>
/// Value object que representa um CEP
/// </summary>
public record CEP
{
    public int Valor {  get; private set; }

    /// <summary>
    /// Esse construtor deveria ser privado, mas é público por conta do EF
    /// </summary>
    /// <param name="valor">Número do CEP</param>
    public CEP(int valor) => Valor = valor;

    /// <summary>
    /// Método fábrica que valida e cria o Dinheiro
    /// </summary>
    /// <param name="valor">Número do CEP</param>
    /// <returns>CEP ou null, caso número ´não represente um CEP</returns>
    public static CEP? Create(int valor) => valor >= 1000000 && valor <= 99999999 ? new CEP(valor) : null;
}
