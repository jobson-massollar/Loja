namespace Loja.Domain;

/// <summary>
/// Value Object que representa um CPF
/// </summary>
public record CPF
{
    public long Valor {  get; init; }

    /// <summary>
    /// Esse construtor deveria ser privado, mas é protegido por conta do EF
    /// </summary>
    /// <param name="valor">Valor numérico do CPF</param>
    protected CPF(long valor) => Valor = valor;

    /// <summary>
    /// Método fábrica que valida e cria o CPF
    /// </summary>
    /// <param name="valor">Valor numérico do CPF</param>
    /// <returns>CPF ou nulo, se número não representa um CPF válido</returns>
    public static CPF? of(long valor) =>  validaCPF(valor) ? new CPF(valor) : null;

    /// <summary>
    /// Valida o CPF
    /// </summary>
    /// <param name="valor">Valor numérico do CPF</param>
    /// <returns>True se número é um CPF válido, ou False caso contrário</returns>
    private static bool validaCPF(long valor)
    {
        long primeiroDV, segundoDV, soma, resto, j, k;
        long numeroCPF;
        long[] mult = [ 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 ];

        // Se cpf não está no intervalo válido ou se tem todos os dígitos iguais
        // então é inválido
        if (valor < 10000000000L || valor > 99999999999L || valor % 11111111111L == 0)
            return false;

        // Pega o primeiro e o segundo DV do cpf
        primeiroDV = valor % 100 / 10;
        segundoDV = valor % 10;

        // Calcula primeiro DV
        numeroCPF = valor / 100; // Número do cpf sem os DVs
        soma = 0;
        for (int i = 0; i < 9; i++)
        {
            soma += (numeroCPF % 10) * mult[i];
            numeroCPF /= 10;
        }

        resto = soma % 11;

        if (resto == 0 || resto == 1)
            j = 0;
        else
            j = 11 - resto;

        // Compara o primeiro DV calculado com o informado
        // Se é inválido, então nem precisa calcular o segundo DV
        if (j != primeiroDV)
            return false;

        // Calcula segundo DV
        numeroCPF = valor / 10;  // Número do cpf sem segundo DV
        soma = 0;
        for (int i = 0; i < 10; i++)
        {
            soma += (numeroCPF % 10) * mult[i];
            numeroCPF /= 10;
        }

        resto = soma % 11;

        if (resto == 0 || resto == 1)
            k = 0;
        else
            k = 11 - resto;

        // Compara o segundo DV calculado com o informado
        return k == segundoDV;
    }
}

