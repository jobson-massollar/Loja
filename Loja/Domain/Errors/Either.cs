namespace Loja.Domain.Errors;

/// <summary>
/// Classe Helper para atribuir os valores Left e Right a Either<T, V>
/// </summary>
public class Eiher
{
    public static Either<T, V> OnLeft<T, V>(T left) => (Either<T, V>)left;

    public static Either<T, V> OnRight<T, V>(V right) => (Either<T, V>)right;
}

/// <summary>
/// Classe genérica que armazena um de dois valores possíveis
/// </summary>
/// <typeparam name="T">Tipo do resultado um</typeparam>
/// <typeparam name="V">Tipo do resultado dois</typeparam>
public class Either<T, V>
{
    protected bool isLeft = false;

    public T? Left { get; protected set; }

    public V? Right { get; protected set; }

    public bool IsLeft => isLeft;

    public bool IsRight => !isLeft;

    private Either(T left)
    {
        Left = left;
        Right = default;
        isLeft = true;
    }

    private Either(V right)
    {
        Left = default;
        Right = right;
        isLeft = false;
    }

    public static implicit operator Either<T, V>(T left) => new Either<T, V>(left);

    public static implicit operator Either<T, V>(V right) => new Either<T, V>(right);
}
