namespace Loja.Domain.Errors;

/// <summary>
/// Classe Helper para atribuir os valores a Result<T>
/// </summary>
public class Result
{
    public static Result<T> OnSuccess<T>(T success) => (Result<T>)success;

    public static Result<T> OnError<T>(List<ErroEntidade> failure) => (Result<T>)failure;
}

/// <summary>
/// Classe genérica que armazena um resultado ou uma lista de erros, caso não seja possível gerar o resultado esperado.
/// A lista de erros é sempre do tipo List<ErroEntidade>
/// </summary>
/// <typeparam name="T">Tipo do resultado</typeparam>
public class Result<T>
{
    /// <summary>
    /// Objeto que armazena o resultado: Left armazena o objeto criado (sucesso) ou Right armazena os erros (fracasso)
    /// O tipo de sucesso é T
    /// O tipo de fracasso é sempre List<ErroEntidade>
    /// </summary>
    private Either<T, List<ErroEntidade>> result;

    /// <summary>
    /// Retorna o resultado ou null, se ele não existe
    /// </summary>
    public T? Value => result.Left;

    /// <summary>
    /// Retorna a lista de erros ou null, se ela não existe
    /// </summary>
    public List<ErroEntidade>? Errors => result.Right;

    /// <summary>
    /// Indica se o resultado foi gerado com sucesso
    /// </summary>
    public bool IsSuccess => result.IsLeft;

    /// <summary>
    /// Indica se houve erro na geração do resultado esperado
    /// </summary>
    public bool hasErrors => result.IsRight;

    /// <summary>
    /// Cria um objeto com o reultado esperado (operação bem sucedida)
    /// </summary>
    /// <param name="success"></param>
    private Result(T success) => result = (Either<T, List<ErroEntidade>>)success;

    /// <summary>
    /// Cria um objeto com a lista de erros (operação mal sucedida)
    /// </summary>
    /// <param name="failure"></param>
    private Result(List<ErroEntidade> failure) => result = (Either<T, List<ErroEntidade>>)failure;

    /// <summary>
    /// Conversor implícito de sucesso: converte 'success' em Result<success, null>
    /// </summary>
    /// <param name="success"></param>
    public static implicit operator Result<T>(T success) => new Result<T>(success);

    /// <summary>
    /// Conversor implícito de erro: converte 'errors' em Result<null, errors>
    /// </summary>
    /// <param name="failure"></param>
    public static implicit operator Result<T>(List<ErroEntidade> errors) => new Result<T>(errors);
}
