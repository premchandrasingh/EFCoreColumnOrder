using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RepoSample.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoSample.Configurations
{
    public class ProductConfiguration : ConfigurationBase<Product>
    {
        public override string TableName => "Product";

        public override void ConfigureEntity(EntityTypeBuilder<Product> builder)
        {
            //ConfigureCommonProperties(builder);

            builder.HasKey(e => e.ProductId);
            builder.Property(e => e.ProductId)
                //.HasDefaultValueSql($"NEXT VALUE FOR SEQ_Product_ProductId"); // use sequence
                .ValueGeneratedOnAdd(); // set identity column

            builder.Property(e => e.ProductCode)
                .IsRequired()
                .HasColumnType("nvarchar(10)")
                .HasMaxLength(10);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("nvarchar(256)")
                .HasMaxLength(256);


            builder.Property(e => e.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(9,2)");
        }
    }
}
