namespace Loja.Domain;

/// <summary>
/// Value object que representa um telefone.
/// Valores não são obrigatórios.
/// </summary>
public record Telefone
{
    public int? DDD { get; private set; }

    public int? Numero { get; private set; }

    /// <summary>
    /// Esse construtor não deveria existir, mas existe por conta do EF
    /// </summary>
    protected Telefone() { }

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    /// <param name="ddd">DDD</param>
    /// <param name="numero">Número do telefone</param>
    protected Telefone(int? ddd, int? numero) {  
        DDD = ddd; 
        Numero = numero;
    }

    /// <summary>
    /// Método fábrica que valida e cria um Telefone
    /// </summary>
    /// <param name="ddd">DDD: não obrigatório. Se for fornecido deve ter 2 dígios</param>
    /// <param name="numero">Número do telefone: não obrigatório se DDD é nulo e obrigatório se DDD existe. Se for fornecido deve ter 7 ou 8 dígitos</param>
    /// <returns></returns>
    public static Telefone? Create(int? ddd, int? numero)
    {
        // Se existir, numero deve ter 7 ou 8 dígitos
        if (numero is int valorNumero && (numero < 20000000 || numero > 999999999 || ))
            return null;

        // Se existir, o DDD deve ter 2 dígitos e o número é obrigatório
        if (ddd is int valorDDD && (valorDDD < 10 || valorDDD > 99 || numero is null))
            return null;

        return new Telefone(ddd, numero);
    }
}
