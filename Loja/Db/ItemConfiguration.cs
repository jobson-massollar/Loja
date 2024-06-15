using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;

namespace Loja.Db;

public class ItemConfiguration : EntityConfiguration<Item>
{
    public override void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Itens");
        builder.ComplexProperty(it => it.Preco);

        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
