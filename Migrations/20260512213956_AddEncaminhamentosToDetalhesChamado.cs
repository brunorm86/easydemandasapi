using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class AddEncaminhamentosToDetalhesChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Encaminhamentos",
                table: "DetalhesChamados",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Encaminhamentos",
                table: "DetalhesChamados");
        }
    }
}
