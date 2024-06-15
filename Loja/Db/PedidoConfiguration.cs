using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;

namespace Loja.Db;

public class PedidoConfiguration : EntityConfiguration<Pedido>
{
    public override void Configure(EntityTypeBuilder<Pedido> builder)
    {
        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
