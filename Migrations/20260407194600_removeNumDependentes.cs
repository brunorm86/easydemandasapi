using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class removeNumDependentes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumDependentes",
                table: "Pessoas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumDependentes",
                table: "Pessoas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
