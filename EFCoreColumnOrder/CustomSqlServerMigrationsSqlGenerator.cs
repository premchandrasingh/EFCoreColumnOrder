using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;

namespace Microsoft.EntityFrameworkCore.Migrations
{
    public class CustomSqlServerMigrationsSqlGenerator : SqlServerMigrationsSqlGenerator
    {
        public CustomSqlServerMigrationsSqlGenerator(
           MigrationsSqlGeneratorDependencies dependencies,
           IMigrationsAnnotationProvider migrationsAnnotations)
       : base(dependencies, migrationsAnnotations)
        {
        }
        
        protected override void CreateTableColumns(CreateTableOperation operation, IModel model, MigrationCommandListBuilder builder)
        {
            operation.Columns.Sort(new ColumnOrderComparision());
            base.CreateTableColumns(operation, model, builder);
        }

        internal class ColumnOrderComparision : IComparer<AddColumnOperation>
        {
            public int Compare(AddColumnOperation x, AddColumnOperation y)
            {
                var orderX = Convert.ToInt32(x.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
                var orderY = Convert.ToInt32(y.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
                return orderX.CompareTo(orderY);
            }
        }
    }
}
