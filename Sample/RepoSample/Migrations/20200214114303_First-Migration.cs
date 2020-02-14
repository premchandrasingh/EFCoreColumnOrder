using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoSample.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    OrderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getUTCDate())")
                        .Annotation("ColumnOrder", 101),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValueSql: "(CURRENT_USER)")
                        .Annotation("ColumnOrder", 102),
                    ModifiedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getUTCDate())")
                        .Annotation("ColumnOrder", 103),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValueSql: "(CURRENT_USER)")
                        .Annotation("ColumnOrder", 104),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getUTCDate())")
                        .Annotation("ColumnOrder", 101),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValueSql: "(CURRENT_USER)")
                        .Annotation("ColumnOrder", 102),
                    ModifiedDateUTC = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getUTCDate())")
                        .Annotation("ColumnOrder", 103),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: false, defaultValueSql: "(CURRENT_USER)")
                        .Annotation("ColumnOrder", 104),
                    ProductCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");
        }
    }
}
