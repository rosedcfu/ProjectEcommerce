using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectEcommerce.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDecimalToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReduceStock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReduceStock",
                table: "Products");
        }
    }
}
