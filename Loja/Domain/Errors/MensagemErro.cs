namespace Loja.Domain.Errors;

/// <summary>
/// Mensagens de erro associadas aos erros de validação das entidades
/// </summary>
public static class MensagemErro
{
    private static readonly Dictionary<ErroEntidade, string> erros = new()
        {
           { ErroEntidade.UF_SIGLA_INVALIDA, "Sigla da UF deve ter 2 caracteres" },
           { ErroEntidade.UF_NOME_INVALIDO, "Nome da UF inválido" },
           { ErroEntidade.UF_SIGLA_JA_EXISTE, "Já existe uma UF com essa sigla" },

           { ErroEntidade.ENDERECO_LOGRADOURO_INVALIDO, "Logradouro não pode ficar em branco" },
           { ErroEntidade.ENDERECO_NUMERO_INVALIDO, "Número inválido" },
           { ErroEntidade.ENDERECO_COMPLEMENTO_INVALIDO, "Complemento inválido" },
           { ErroEntidade.ENDERECO_BAIRRO_INVALIDO, "Bairro não pode ficar em branco" },
           { ErroEntidade.ENDERECO_CEP_INVALIDO, "CEP inválido" },
           { ErroEntidade.ENDERECO_UF_INVALIDA, "UF inválida" },

           { ErroEntidade.CLIENTE_CPF_INVALIDO, "CPF do cliente inválido" },
           { ErroEntidade.CLIENTE_NOME_INVALIDO, "Nome do cliente não pode ficar em branco" },
           { ErroEntidade.CLIENTE_EMAIL_INVALIDO, "E-mail do cliente inválido" },
           { ErroEntidade.CLIENTE_ENDERECO_INVALIDO, "Endereço do cliente não fornecido" },
           { ErroEntidade.CLIENTE_TELEFONE_INVALIDO, "Telefone do cliente inválido. Forneça DDD e número" },
           { ErroEntidade.CLIENTE_CPF_JA_EXISTE, "Já existe um cliente com esse CPF" },
           { ErroEntidade.CLIENTE_EMAIL_JA_EXISTE, "Já existe um cliente com esse e-mail" },

           { ErroEntidade.PRODUTO_CODIGO_BARRAS_INVALIDO, "Código de barras do produto deve ter 13 dígitos" },
           { ErroEntidade.PRODUTO_DESCRICAO_INVALIDA, "Descrição do produto não pode ficar em branco" },
           { ErroEntidade.PRODUTO_PRECO_INVALIDO, "Preço do produto inválido" },

           { ErroEntidade.PEDIDO_CLIENTE_INVALIDO, "Cliente do pedido inválido" },
           { ErroEntidade.PEDIDO_ENDERECO_INVALIDO, "Endereço de entrega do pedido inválido" },

           { ErroEntidade.ITEM_PEDIDO_INVALIDO, "Item com pedido inválido" },
           { ErroEntidade.ITEM_PRODUTO_INVALIDO, "Item com produto inválido" },
           { ErroEntidade.ITEM_QUANTIDADE_INVALIDA, "Quantidde do item deve ser maior que zero" },
           { ErroEntidade.ITEM_PRECO_INVALIDO, "Preço do item inválido" },

           { ErroEntidade.PREFERENCIA_DESCRICAO_INVALIDA, "Descrição da preferência não pode ficar em branco" },
           { ErroEntidade.PREFERENCIA_DESCRICAO_JA_EXISTE, "Já existe uma preferência com essa descrição" }
        };

    public static string Get(ErroEntidade erro) => erros.TryGetValue(erro, out string? result) ? result : erro.ToString();
}
