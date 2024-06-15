using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;

namespace Loja.Db;

public class ProdutoConfiguration : EntityConfiguration<Produto>
{
    public override void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.Property(p => p.Descricao).HasMaxLength(50);

        builder.ComplexProperty(p => p.CodigoBarras);

        builder.ComplexProperty(p => p.Preco);

        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
