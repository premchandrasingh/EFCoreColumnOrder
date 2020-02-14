using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepoSample.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Configurations
{
    public abstract class ConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public virtual string SchemaName { get; } = "dbo";

        public abstract string TableName { get; }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(TableName, SchemaName);

            ConfigureEntity(builder);

            builder.Property(e => e.CreatedDateUTC)
               .IsRequired()
               .HasDefaultValueSql("(getUTCDate())")
               .HasColumnType("datetime")
               .HasColumnOrder(101);

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasDefaultValueSql("(CURRENT_USER)")
                .HasColumnType("nvarchar(100)")
                .HasColumnOrder(102);

            builder.Property(e => e.ModifiedDateUTC)
                .IsRequired()
                .HasDefaultValueSql("(getUTCDate())")
                .HasColumnType("datetime")
                .HasColumnOrder(103);

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasDefaultValueSql("(CURRENT_USER)")
                .HasColumnType("nvarchar(100)")
                .HasColumnOrder(104);
        }
    }
}
