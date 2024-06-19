using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;
using Microsoft.EntityFrameworkCore;

namespace Loja.Db;

public class ClienteConfiguration : EntityConfiguration<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ComplexProperty(c => c.Telefone);

        builder.Property(c => c.Nome).HasMaxLength(100);

        builder.Property(c => c.Email).HasMaxLength(100);

        builder.HasIndex(c => c.Email).IsUnique();

        builder.HasOne(c => c.Endereco).WithOne(e => e.Cliente).OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(p => p.CPF, cpfBuilder =>
        {
            cpfBuilder.Property(cpf => cpf.Valor);
            cpfBuilder.HasIndex(cpf => cpf.Valor).IsUnique();
        });

        // É importante chamar por último, porque o configurador base vai usar o nome da tabela
        // para gerar o trigger de update
        base.Configure(builder);
    }
}
