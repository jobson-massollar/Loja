using Loja.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Db;

public class CategoriaConfiguration : EntityConfiguration<Categoria>
{
    public override void Configure(EntityTypeBuilder<Categoria> builder)
    {
        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
