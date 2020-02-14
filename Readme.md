## Purpose
Till EF Core 3.1, Database column ordering for migration is not available out of the box. Many people are trying to figure out their custom one. This is one custom ordering which I think much cleaner.

Ref:-
- https://github.com/dotnet/efcore/issues/11314
- https://github.com/dotnet/efcore/issues/2272
- https://github.com/bricelam/EFCore/commit/ed629d65089bc7b1bbd6853c335e541df5a5ae7e

## How to use
- Copy files from `EFCoreColumnOrder` projects and paste in your project. Don't worry about namespace, all classes included in their original namespaces
- Simply use `HasColumnOrder(<number>)` in your model configuration

## Sample

### Entities
```C#
    public class EntityBase
    {
        public DateTime CreatedDateUTC { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDateUTC { get; set; }

        public string ModifiedBy { get; set; }
    }

    public class Order : EntityBase
    {
        public long OrderId { get; set; }

        public long ProductId { get; set; }

        public int Units { get; set; }

        public decimal UnitPrice { get; set; }

    }

```

### Configurations
```C#
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
               .HasColumnOrder(101); //<==============

            builder.Property(e => e.CreatedBy)
                .IsRequired()
                .HasDefaultValueSql("(CURRENT_USER)")
                .HasColumnType("nvarchar(100)")
                .HasColumnOrder(102); //<==============

            builder.Property(e => e.ModifiedDateUTC)
                .IsRequired()
                .HasDefaultValueSql("(getUTCDate())")
                .HasColumnType("datetime")
                .HasColumnOrder(103); //<==============

            builder.Property(e => e.ModifiedBy)
                .IsRequired()
                .HasDefaultValueSql("(CURRENT_USER)")
                .HasColumnType("nvarchar(100)")
                .HasColumnOrder(104); //<==============
        }
    }


     public class OrderConfiguration : ConfigurationBase<Order>
    {
        public override string TableName => nameof(Order);

        public override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            //ConfigureCommonProperties(builder);

            builder.HasKey(e => e.OrderId);
            builder.Property(e => e.OrderId)
                //.HasDefaultValueSql($"NEXT VALUE FOR SEQ_Order_OrderId"); // use sequence
                .ValueGeneratedOnAdd(); // set identity column

            builder.Property(e => e.ProductId)
               .IsRequired()
               .HasColumnType("bigint");

            builder.Property(e => e.Units)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(9,2)");
        }
    }
```

### Output
![Generated C# migration script](images/migration1.png)

![Final DB](images/migration2.png)