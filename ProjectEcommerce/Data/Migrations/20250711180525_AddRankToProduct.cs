using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectEcommerce.Data.Migrations
{
    public partial class AddRankToProduct : Migration
    {
        /// Añadir ranking de productos reales
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rank",
                table: "Products",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// Eliminar ranking de productos
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Products");
        }
    }
}
