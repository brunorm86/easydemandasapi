using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class SeedIndeterminados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 9999, "CARGO INDETERMINADO" });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Cpf", "DataNascimento", "Email", "Endereco", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 9999, "00000000000", new DateOnly(1900, 1, 1), "indeterminado@easydemandas.com", "Indeterminado", "PESSOA", "INDETERMINADA", "00000000000" });

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "Id", "CargoId", "DataContratacao", "DepartamentoId", "PessoaId" },
                values: new object[] { 9999, 9999, new DateOnly(1900, 1, 1), null, 9999 });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "Nome", "ResponsavelId", "Sigla" },
                values: new object[] { 9999, "DEPARTAMENTO INDETERMINADO", 9999, "IND" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 9999);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 9999);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 9999);
        }
    }
}
