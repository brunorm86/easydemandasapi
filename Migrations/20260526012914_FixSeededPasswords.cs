using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class FixSeededPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9001,
                column: "SenhaHash",
                value: "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9002,
                column: "SenhaHash",
                value: "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9003,
                column: "SenhaHash",
                value: "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9004,
                column: "SenhaHash",
                value: "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                column: "SenhaHash",
                value: "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9001,
                column: "SenhaHash",
                value: "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9002,
                column: "SenhaHash",
                value: "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9003,
                column: "SenhaHash",
                value: "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9004,
                column: "SenhaHash",
                value: "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                column: "SenhaHash",
                value: "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139");
        }
    }
}
