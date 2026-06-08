using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GerenciamentoDeFrota.Migrations
{
    /// <inheritdoc />
    public partial class VeiculosCompleto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "KmAtual",
                table: "Veiculos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Fabricante",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "Veiculos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Veiculos",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Antt",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AnttDtVencimento",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CNPJ",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CapacidadeCaixa",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CapacidadePaletes",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CapacidadeTanque",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Carroceria",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CentrosCustoId",
                table: "Veiculos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CronoacografoDtVencimento",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtintorCodigo",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtintorDtVencimento",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ipva",
                table: "Veiculos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IpvaDtVencimento",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KmHora",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Licenciamento",
                table: "Veiculos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenciamentoDtVencimento",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LotacaoKg",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumeroFrota",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Padronizacao",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Proprietario",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SeguroDtInicioVigencia",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SeguroDtTerminoVigencia",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeguroNrApolice",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeguroSeguradora",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeguroTipo",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaraKg",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Terceirizado",
                table: "Veiculos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UF",
                table: "Veiculos",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorFipe",
                table: "Veiculos",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VeiculoTracao",
                table: "Veiculos",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "CentrosCusto",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "CentrosCusto",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "CentrosCusto",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "CentrosCusto",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Servico",
                table: "AgendamentosManutencao",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "AgendamentosManutencao",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HorarioAgendamento",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAgendamento",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateTable(
                name: "VeiculosAnexos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VeiculoId = table.Column<long>(type: "bigint", nullable: false),
                    NomeArquivo = table.Column<string>(type: "longtext", nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "longtext", nullable: false),
                    TipoArquivo = table.Column<string>(type: "longtext", nullable: true),
                    TamanhoBytes = table.Column<long>(type: "bigint", nullable: true),
                    DataUpload = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosAnexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculosAnexos_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VeiculosDocumentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    VeiculoId = table.Column<long>(type: "bigint", nullable: false),
                    Documento = table.Column<string>(type: "longtext", nullable: false),
                    DtVencimento = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculosDocumentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculosDocumentos_Veiculos_VeiculoId",
                        column: x => x.VeiculoId,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_CentrosCustoId",
                table: "Veiculos",
                column: "CentrosCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosAnexos_VeiculoId",
                table: "VeiculosAnexos",
                column: "VeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculosDocumentos_VeiculoId",
                table: "VeiculosDocumentos",
                column: "VeiculoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_CentrosCusto_CentrosCustoId",
                table: "Veiculos",
                column: "CentrosCustoId",
                principalTable: "CentrosCusto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_CentrosCusto_CentrosCustoId",
                table: "Veiculos");

            migrationBuilder.DropTable(
                name: "VeiculosAnexos");

            migrationBuilder.DropTable(
                name: "VeiculosDocumentos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_CentrosCustoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Antt",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "AnttDtVencimento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CapacidadeCaixa",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CapacidadePaletes",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CapacidadeTanque",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Carroceria",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CentrosCustoId",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "CronoacografoDtVencimento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "ExtintorCodigo",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "ExtintorDtVencimento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Ipva",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "IpvaDtVencimento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "KmHora",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Licenciamento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "LicenciamentoDtVencimento",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "LotacaoKg",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "NumeroFrota",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Padronizacao",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Proprietario",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "SeguroDtInicioVigencia",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "SeguroDtTerminoVigencia",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "SeguroNrApolice",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "SeguroSeguradora",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "SeguroTipo",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "TaraKg",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "Terceirizado",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "UF",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "ValorFipe",
                table: "Veiculos");

            migrationBuilder.DropColumn(
                name: "VeiculoTracao",
                table: "Veiculos");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Veiculos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Renavam",
                table: "Veiculos",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Veiculos",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "Veiculos",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Modelo",
                table: "Veiculos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KmAtual",
                table: "Veiculos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fabricante",
                table: "Veiculos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Veiculos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "Veiculos",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Veiculos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "CentrosCusto",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "CentrosCusto",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "CentrosCusto",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "CentrosCusto",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Servico",
                table: "AgendamentosManutencao",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "AgendamentosManutencao",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HorarioAgendamento",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAgendamento",
                table: "AgendamentosManutencao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }
    }
}
