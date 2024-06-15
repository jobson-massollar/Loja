using System.Text.RegularExpressions;

namespace Loja.Domain;

/// <summary>
/// Value Object que representa um código de barras
/// </summary>
public record CodigoBarras
{
    /// <summary>
    /// Valor numérico do código de barras
    /// </summary>
    public string Valor { get; init; }

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    /// <param name="valor">Valor do código de barras</param>
    protected CodigoBarras(string valor) => Valor = valor;

    /// <summary>
    /// Método fábrica que valida e constrói o código de barras
    /// </summary>
    /// <param name="valor">Valor do código de barras (número com 13 dígitos)</param>
    /// <returns>Código de barras ou nulo, se valor numérico é inválido</returns>
    public static CodigoBarras? Create(string valor) => 
        valor != null && Regex.IsMatch(valor, @"^\d{13}$") ? new CodigoBarras(valor) : null;
}
