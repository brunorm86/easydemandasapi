using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthToEmpregado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Perfil",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenhaHash",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "Perfil", "SenhaHash" },
                values: new object[] { "Gestor", "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139" });

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "Id", "CargoId", "Cpf", "DataContratacao", "DataNascimento", "DepartamentoId", "Email", "Endereco", "Nome", "Perfil", "SenhaHash", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 9001, 9999, "11111111111", new DateOnly(2020, 1, 1), new DateOnly(1985, 5, 10), null, "gestor@easydemandas.com", "Rua do Gestor, 123", "Gestor", "Gestor", "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139", "Plataforma", "11999999999" },
                    { 9002, 9999, "22222222222", new DateOnly(2021, 2, 1), new DateOnly(1990, 6, 15), null, "rh@easydemandas.com", "Rua do RH, 123", "RH", "RH", "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139", "Usuario", "11888888888" },
                    { 9003, 9999, "33333333333", new DateOnly(2022, 3, 1), new DateOnly(1992, 8, 20), null, "suporte@easydemandas.com", "Rua do Suporte, 123", "Suporte", "Suporte", "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139", "Chamados", "11777777777" },
                    { 9004, 9999, "44444444444", new DateOnly(2023, 4, 1), new DateOnly(1995, 10, 25), null, "usuario@easydemandas.com", "Rua do Usuario, 123", "Usuario", "Usuario", "8d969eee76d92476d701125b16222e1785c874291147139c74c09044b1551139", "Comum", "11666666666" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9001);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9002);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9003);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9004);

            migrationBuilder.DropColumn(
                name: "Perfil",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "SenhaHash",
                table: "Empregados");
        }
    }
}
