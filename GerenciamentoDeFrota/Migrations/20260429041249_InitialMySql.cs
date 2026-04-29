using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GerenciamentoDeFrota.Migrations
{
    /// <inheritdoc />
    public partial class InitialMySql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CentrosCusto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosCusto", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Fabricante = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Modelo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    KmAtual = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: true),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: true),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    Renavam = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Placa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    MesEmplacamento = table.Column<int>(type: "int", nullable: true),
                    AnoEmplacamento = table.Column<int>(type: "int", nullable: true),
                    DataTacografo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Cor = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Observacoes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AgendamentosManutencao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VeiculoId = table.Column<long>(type: "bigint", nullable: false),
                    DataAgendamento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorarioAgendamento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Servico = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    KmAtualAgendamento = table.Column<int>(type: "int", nullable: true),
                    Observacoes = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendamentosManutencao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendamentosManutencao_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AgendamentosManutencao_VeiculoId",
                table: "AgendamentosManutencao",
                column: "VeiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendamentosManutencao");

            migrationBuilder.DropTable(
                name: "CentrosCusto");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
