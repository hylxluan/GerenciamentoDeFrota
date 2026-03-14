using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeFrota.Migrations
{
    /// <inheritdoc />
    public partial class VeiculosUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KmAtual",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Veiculos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KmAtual",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Veiculos");
        }
    }
}
