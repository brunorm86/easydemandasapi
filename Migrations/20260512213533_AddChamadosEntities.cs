using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class AddChamadosEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    SolicitanteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamados_Empregados_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Empregados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalhesChamados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChamadoId = table.Column<int>(type: "integer", nullable: false),
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false),
                    Custo = table.Column<decimal>(type: "numeric", nullable: true),
                    NivelCriticidade = table.Column<string>(type: "text", nullable: false),
                    Urgencia = table.Column<string>(type: "text", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesChamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalhesChamados_Chamados_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesChamados_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_SolicitanteId",
                table: "Chamados",
                column: "SolicitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesChamados_ChamadoId",
                table: "DetalhesChamados",
                column: "ChamadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesChamados_DepartamentoId",
                table: "DetalhesChamados",
                column: "DepartamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesChamados");

            migrationBuilder.DropTable(
                name: "Chamados");
        }
    }
}
