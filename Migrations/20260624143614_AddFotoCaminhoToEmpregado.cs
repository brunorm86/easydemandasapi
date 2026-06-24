using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class AddFotoCaminhoToEmpregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoCaminho",
                table: "Empregados",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 1,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 2,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 3,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 4,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 5,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 6,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 7,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 8,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 10,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 11,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 12,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 13,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 14,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 15,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 16,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 17,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 18,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 19,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 20,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 21,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 22,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 23,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 24,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 25,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 26,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 27,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 28,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 29,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 30,
                column: "FotoCaminho",
                value: null);

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                column: "FotoCaminho",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoCaminho",
                table: "Empregados");
        }
    }
}
