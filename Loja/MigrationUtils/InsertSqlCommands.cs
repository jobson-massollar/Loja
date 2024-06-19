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
        var dropTriggerOperations = new List<SqlOperation>();
        var sourceModel = source?.Model;
        var targetModel = target?.Model;
        var oldEntityTypes = sourceModel?.GetEntityTypes();
        var newEntityTypes = targetModel?.GetEntityTypes();
        var commonEntityTypes = oldEntityTypes is null || newEntityTypes is null ? (List<IEntityType>)[] : oldEntityTypes.Intersect(newEntityTypes).ToList();
        var deletedEntityTypes = oldEntityTypes is not null ? oldEntityTypes.Except(commonEntityTypes).ToList() : (List<IEntityType>)[];
        var insertedEntityTypes = newEntityTypes is not null ? newEntityTypes.Except(commonEntityTypes).ToList() : (List<IEntityType>)[];

        if (deletedEntityTypes != null)
        {
            foreach (var entity in deletedEntityTypes)
            {
                try
                {
                    var annotation = entity.GetAnnotation("DROP-UPDATE-TRIGGER-" + entity.GetTableName());

                    if (annotation is not null && annotation.Value is string str)
                        dropTriggerOperations.Add(new SqlOperation { Sql = str });
                }
                catch (Exception _)
                {
                }

            }
        }

        if (newEntityTypes != null)
        {
            foreach (var entity in newEntityTypes)
            {
                try
                {
                    var annotation = entity.GetAnnotation("CREATE-UPDATE-TRIGGER-" + entity.GetTableName());

                    if (annotation is not null && annotation.Value is string str)
                        createTriggerOperations.Add(new SqlOperation { Sql = str });
                }
                catch (Exception _)
                {
                }

            }
        }

        return new List<MigrationOperation>(dropTriggerOperations).Concat(operations).Concat(createTriggerOperations).ToList();
    }
}
