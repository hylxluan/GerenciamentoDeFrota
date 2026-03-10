using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeFrota.Migrations
{
    /// <inheritdoc />
    public partial class Veiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fabricante = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnoModelo = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    AnoFabricacao = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Renavam = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Placa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MesEmplacamento = table.Column<int>(type: "int", nullable: true),
                    AnoEmplacamento = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    DataTacografo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
