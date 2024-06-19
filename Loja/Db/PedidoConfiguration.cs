using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;
using Microsoft.EntityFrameworkCore;

namespace Loja.Db;

public class PedidoConfiguration : EntityConfiguration<Pedido>
{
    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.HasOne(p => p.EnderecoEntrega).WithOne(e => e.Pedido).OnDelete(DeleteBehavior.Cascade);

        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
