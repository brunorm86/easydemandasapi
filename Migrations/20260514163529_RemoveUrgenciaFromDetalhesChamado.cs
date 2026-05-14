using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUrgenciaFromDetalhesChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Urgencia",
                table: "DetalhesChamados");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Urgencia",
                table: "DetalhesChamados",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
