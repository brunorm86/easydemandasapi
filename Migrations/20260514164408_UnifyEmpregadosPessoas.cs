using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class UnifyEmpregadosPessoas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Adiciona as novas colunas de dados pessoais em Empregados
            //    (com defaultValue vazio para linhas existentes)
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataNascimento",
                table: "Empregados",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Empregados",
                type: "text",
                nullable: false,
                defaultValue: "");

            // 2. Copia os dados pessoais de Pessoas para Empregados (preserva histórico)
            migrationBuilder.Sql(@"
                UPDATE ""Empregados"" e
                SET ""Nome""          = p.""Nome"",
                    ""Sobrenome""     = p.""Sobrenome"",
                    ""Email""         = p.""Email"",
                    ""Telefone""      = p.""Telefone"",
                    ""Endereco""      = p.""Endereco"",
                    ""Cpf""           = p.""Cpf"",
                    ""DataNascimento"" = p.""DataNascimento""
                FROM ""Pessoas"" p
                WHERE e.""PessoaId"" = p.""Id"";
            ");

            // 3. Atualiza o seed do empregado indeterminado (Id 9999)
            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                columns: new[] { "Cpf", "DataNascimento", "Email", "Endereco", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { "00000000000", new DateOnly(1900, 1, 1), "indeterminado@easydemandas.com", "Indeterminado", "EMPREGADO", "INDETERMINADO", "00000000000" });

            // 4. Remove FK de Empregados -> Pessoas
            migrationBuilder.DropForeignKey(
                name: "FK_Empregados_Pessoas_PessoaId",
                table: "Empregados");

            // 5. Remove tabela Dependentes (tem FKs para Empregados e Pessoas)
            migrationBuilder.DropTable(
                name: "Dependentes");

            // 6. Remove tabela Pessoas (FKs já foram removidas acima)
            migrationBuilder.DropTable(
                name: "Pessoas");

            // 7. Remove índice e coluna PessoaId de Empregados
            migrationBuilder.DropIndex(
                name: "IX_Empregados_PessoaId",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Empregados");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Empregados");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Empregados");

            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Empregados",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "text", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Sobrenome = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpregadoId = table.Column<int>(type: "integer", nullable: false),
                    PessoaId = table.Column<int>(type: "integer", nullable: false),
                    Parentesco = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependentes_Empregados_EmpregadoId",
                        column: x => x.EmpregadoId,
                        principalTable: "Empregados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependentes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9999,
                column: "PessoaId",
                value: 9999);

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Cpf", "DataNascimento", "Email", "Endereco", "Nome", "Sobrenome", "Telefone" },
                values: new object[] { 9999, "00000000000", new DateOnly(1900, 1, 1), "indeterminado@easydemandas.com", "Indeterminado", "PESSOA", "INDETERMINADA", "00000000000" });

            migrationBuilder.CreateIndex(
                name: "IX_Empregados_PessoaId",
                table: "Empregados",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependentes_EmpregadoId",
                table: "Dependentes",
                column: "EmpregadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependentes_PessoaId",
                table: "Dependentes",
                column: "PessoaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Empregados_Pessoas_PessoaId",
                table: "Empregados",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
