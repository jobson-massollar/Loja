using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Loja.Domain;

namespace Loja.Db;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T> 
    where T : Entity<T>
{
    private readonly string sufixoNomeTrigger = "AfterUpdate";

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("datetime()");
        builder.Property(e => e.UpdatedAt).HasDefaultValueSql("datetime()");

        addTriggers(builder);
    }

    private void addTriggers(EntityTypeBuilder<T> builder)
    {
        var tipoEntidade = builder.Metadata.Model.FindEntityType(typeof(T).FullName!);

        if (tipoEntidade is not null)
        {
            var nomeTabela = builder.Metadata.GetTableName();

            var trigger = @$"
DROP TRIGGER IF EXISTS {nomeTabela}_{sufixoNomeTrigger};
CREATE TRIGGER IF NOT EXISTS {nomeTabela}_{sufixoNomeTrigger} 
         AFTER UPDATE
            ON {nomeTabela}
          WHEN old.UpdatedAt <> CURRENT_TIMESTAMP
BEGIN
    UPDATE {nomeTabela}
       SET UpdatedAt = CURRENT_TIMESTAMP
     WHERE id = OLD.id;
END;
";

            tipoEntidade.AddAnnotation("UPDATE-TRIGGER-" + nomeTabela, trigger);
            tipoEntidade.AddTrigger($"{nomeTabela}_{sufixoNomeTrigger}");
        }
    }
}
