using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Loja.MigrationUtils;

public class InsertSqlCommands : Microsoft.EntityFrameworkCore.Migrations.Internal.MigrationsModelDiffer
{
    public InsertSqlCommands(
        IRelationalTypeMappingSource typeMappingSource,
        IMigrationsAnnotationProvider migrationsAnnotationProvider,
        IRowIdentityMapFactory rowIdentityMapFactory,
        CommandBatchPreparerDependencies commandBatchPreparerDependencies)
        : base(typeMappingSource, migrationsAnnotationProvider, rowIdentityMapFactory, commandBatchPreparerDependencies)
    {
    }

    public override IReadOnlyList<MigrationOperation> GetDifferences(IRelationalModel? source, IRelationalModel? target)
    {
        var operations = base.GetDifferences(source, target);

        var createTriggerOperations = new List<SqlOperation>();
        var targetModel = target?.Model;
        var entityTypes = targetModel?.GetEntityTypes();

        if (entityTypes != null)
        {
            foreach (var entity in entityTypes)
            {
                try
                {
                    var annotation = entity.GetAnnotation("UPDATE-TRIGGER-" + entity.GetTableName());

                    if (annotation is not null && annotation.Value is string str)
                    {
                        var sql = new SqlOperation
                        {
                            Sql = str
                        };
                        createTriggerOperations.Add(sql);
                    }
                }
                catch (Exception _)
                {
                }

            }
        }

        return new List<MigrationOperation>(operations).Concat(createTriggerOperations).ToList();
    }
}
