using Loja.Domain.Errors;

namespace Loja.Domain;

public class EnderecoBuilder
{
    protected string? logradouro;
    protected string? numero;
    protected string? complemento;
    protected string? bairro;
    protected int? numeroCEP;
    protected UF? uf;

    public EnderecoBuilder() { }

    public EnderecoBuilder(string logradouro, string numero, string complemento, string bairro, int numeroCEP, UF uf)
    {
        this.logradouro = logradouro;
        this.numero = numero;
        this.complemento = complemento;
        this.bairro = bairro;
        this.numeroCEP = numeroCEP;
        this.uf = uf;
    }

    public EnderecoBuilder ComLogradouro(string logradouro)
    {
        this.logradouro = logradouro;
        return this;
    }

    public EnderecoBuilder ComNumero(string numero)
    {
        this.numero = numero;
        return this;
    }

    public EnderecoBuilder ComComplemento(string complemento)
    {
        this.complemento = complemento;
        return this;
    }

    public EnderecoBuilder ComBairro(string bairro)
    {
        this.bairro = bairro;
        return this;
    }

    public EnderecoBuilder ComCEP(int cep)
    {
        numeroCEP = cep;
        return this;
    }

    /// <summary>
    /// Define a UF do endereço. Diferentemente dos demais, esse objeto já vem construído porque ele
    /// deve ser gerado fora do builder.
    /// </summary>
    /// <param name="uf">UF do endereço</param>
    /// <returns>ClienteBuilder para poder implementar a fluent API</returns>
    public EnderecoBuilder ComUF(UF uf)
    {
        this.uf = uf;
        return this;
    }

    public Result<Endereco> Build()
    {
        var cep = numeroCEP is int _cep ? CEP.Create(_cep) : null;
        var result = Endereco.Create(logradouro, numero, complemento, bairro, cep, uf);

        return result.IsSuccess ? result.Value! : result.Errors!;
    }
}
