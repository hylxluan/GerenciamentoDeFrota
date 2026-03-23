using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeFrota.Migrations
{
    /// <inheritdoc />
    public partial class AddKmAtualAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KmAtualAgendamento",
                table: "AgendamentosManutencao",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KmAtualAgendamento",
                table: "AgendamentosManutencao");
        }
    }
}
