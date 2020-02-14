using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepoSample.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Configurations
{
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
}
