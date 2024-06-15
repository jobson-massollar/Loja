namespace Loja.Domain;

/// <summary>
/// Supper classe das Entidades.
/// Define as propriedades comuns e força a implementação do Equals.
/// </summary>
/// <typeparam name="T">Entidade do sistema</typeparam>
public abstract class Entity<T>: IEquatable<T> where T : Entity<T>
{
    /// <summary>
    /// Id do objeto (usado como Pk no mecanismo de persistência)
    /// Valor é criado por cada instância
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Data da criação do objeto.
    /// Não é usado pelo modelo OO.
    /// Foi definido apenas para ser usado apenas pelo mecanismo de persistência.
    /// </summary>
    public DateTime CreatedAt { get; protected set;  }

    /// <summary>
    /// Data de atualização do objeto.
    /// Não é usado pelo modelo OO.
    /// Foi definido apenas para ser usado apenas pelo mecanismo de persistência.
    /// </summary>
    public DateTime UpdatedAt { get; protected set; }

    /// <summary>
    /// Método Equals abstrato para forçar a implementação desse método nas Entidades
    /// </summary>
    /// <param name="outro">Outro objeto a ser comparado</param>
    /// <returns>True, se os objetos são iguais; ou False, caso contrário</returns>
    public abstract bool Equals(T? outro);

    /// <summary>
    /// Método Equals herdado de object.
    /// Chama o método Equals de IEquatable
    /// </summary>
    /// <param name="obj">Outro objeto a ser comparado</param>
    /// <returns>True, se os objetos são iguais; ou False, caso contrário</returns>
    public override bool Equals(object? obj) => obj is T o && Equals(o);

    /// <summary>
    /// Hash do objeto baseado no Id
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => Id.GetHashCode();
}
