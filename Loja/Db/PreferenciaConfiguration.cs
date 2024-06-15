using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;

namespace Loja.Db;

public class PreferenciaConfiguration : EntityConfiguration<Preferencia>
{
    public override void Configure(EntityTypeBuilder<Preferencia> builder)
    {
        builder.HasIndex(p => p.Descricao).IsUnique();

        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
