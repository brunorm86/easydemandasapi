using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace easydemandasapi.Migrations
{
    /// <inheritdoc />
    public partial class PopulateCleanDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE \"DetalhesChamados\", \"Chamados\", \"Empregados\", \"Departamentos\", \"Cargos\" RESTART IDENTITY CASCADE;");
            migrationBuilder.Sql("INSERT INTO \"Cargos\" (\"Id\", \"Nome\") VALUES (9999, 'CARGO INDETERMINADO');");
            migrationBuilder.Sql("INSERT INTO \"Empregados\" (\"Id\", \"Nome\", \"Sobrenome\", \"Email\", \"Telefone\", \"Endereco\", \"Cpf\", \"DataNascimento\", \"CargoId\", \"DataContratacao\", \"DepartamentoId\", \"SenhaHash\", \"Perfil\") VALUES (9999, 'EMPREGADO', 'INDETERMINADO', 'indeterminado@easydemandas.com', '00000000000', 'Indeterminado', '00000000000', '1900-01-01', 9999, '1900-01-01', NULL, '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Gestor');");
            migrationBuilder.Sql("INSERT INTO \"Departamentos\" (\"Id\", \"Nome\", \"Sigla\", \"ResponsavelId\") VALUES (9999, 'DEPARTAMENTO INDETERMINADO', 'IND', 9999);");

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

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Diretor" },
                    { 2, "Gerente" },
                    { 3, "Analista" },
                    { 4, "Técnico" },
                    { 5, "Assistente" }
                });

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "Id", "CargoId", "Cpf", "DataContratacao", "DataNascimento", "DepartamentoId", "Email", "Endereco", "Nome", "Perfil", "SenhaHash", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, 1, "00000000001", new DateOnly(2016, 2, 2), new DateOnly(1981, 2, 2), null, "ana.santos@easydemandas.com", "Rua das Flores, 101", "Ana", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Santos", "11900000001" },
                    { 2, 2, "00000000002", new DateOnly(2017, 3, 3), new DateOnly(1982, 3, 3), null, "bruno.oliveira@easydemandas.com", "Rua das Flores, 102", "Bruno", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Oliveira", "11900000002" },
                    { 3, 3, "00000000003", new DateOnly(2018, 4, 4), new DateOnly(1983, 4, 4), null, "carla.pereira@easydemandas.com", "Rua das Flores, 103", "Carla", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Pereira", "11900000003" },
                    { 4, 4, "00000000004", new DateOnly(2019, 5, 5), new DateOnly(1984, 5, 5), null, "daniel.costa@easydemandas.com", "Rua das Flores, 104", "Daniel", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Costa", "11900000004" },
                    { 5, 5, "00000000005", new DateOnly(2020, 6, 6), new DateOnly(1985, 6, 6), null, "elena.ferreira@easydemandas.com", "Rua das Flores, 105", "Elena", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Ferreira", "11900000005" },
                    { 6, 1, "00000000006", new DateOnly(2021, 7, 7), new DateOnly(1986, 7, 7), null, "felipe.lima@easydemandas.com", "Rua das Flores, 106", "Felipe", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Lima", "11900000006" },
                    { 7, 2, "00000000007", new DateOnly(2022, 8, 8), new DateOnly(1987, 8, 8), null, "gabriela.souza@easydemandas.com", "Rua das Flores, 107", "Gabriela", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Souza", "11900000007" }
                });

            migrationBuilder.InsertData(
                table: "Chamados",
                columns: new[] { "Id", "DataAbertura", "DataConclusao", "Descricao", "SolicitanteId", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 1 aberto para resolução de erro no erp #1. Favor analisar com atenção.", 1, "Aberto", "Erro no ERP #1" },
                    { 2, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 2 aberto para resolução de reset de senha #2. Favor analisar com atenção.", 2, "Em Andamento", "Reset de Senha #2" },
                    { 3, new DateTime(2026, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 3 aberto para resolução de problema com vpn #3. Favor analisar com atenção.", 3, "Concluído", "Problema com VPN #3" },
                    { 4, new DateTime(2026, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 4 aberto para resolução de impressora não funciona #4. Favor analisar com atenção.", 4, "Cancelado", "Impressora Não Funciona #4" },
                    { 5, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 5 aberto para resolução de lentidão na estação #5. Favor analisar com atenção.", 5, "Aberto", "Lentidão na Estação #5" },
                    { 6, new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 6 aberto para resolução de falha de conectividade #6. Favor analisar com atenção.", 6, "Em Andamento", "Falha de Conectividade #6" },
                    { 7, new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 7 aberto para resolução de instalação de office #7. Favor analisar com atenção.", 7, "Concluído", "Instalação de Office #7" },
                    { 31, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 31 aberto para resolução de erro no erp #31. Favor analisar com atenção.", 1, "Concluído", "Erro no ERP #31" },
                    { 32, new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 32 aberto para resolução de reset de senha #32. Favor analisar com atenção.", 2, "Cancelado", "Reset de Senha #32" },
                    { 33, new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 33 aberto para resolução de problema com vpn #33. Favor analisar com atenção.", 3, "Aberto", "Problema com VPN #33" },
                    { 34, new DateTime(2026, 4, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 34 aberto para resolução de impressora não funciona #34. Favor analisar com atenção.", 4, "Em Andamento", "Impressora Não Funciona #34" },
                    { 35, new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 35 aberto para resolução de lentidão na estação #35. Favor analisar com atenção.", 5, "Concluído", "Lentidão na Estação #35" },
                    { 36, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 36 aberto para resolução de falha de conectividade #36. Favor analisar com atenção.", 6, "Cancelado", "Falha de Conectividade #36" },
                    { 37, new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 37 aberto para resolução de instalação de office #37. Favor analisar com atenção.", 7, "Aberto", "Instalação de Office #37" }
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "Nome", "ResponsavelId", "Sigla" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação", 1, "TI" },
                    { 2, "Recursos Humanos", 2, "RH" },
                    { 3, "Financeiro", 3, "FIN" },
                    { 4, "Vendas", 4, "VEN" },
                    { 5, "Marketing", 5, "MKT" },
                    { 6, "Operações", 6, "OPE" },
                    { 7, "Jurídico", 7, "JUR" }
                });

            migrationBuilder.InsertData(
                table: "DetalhesChamados",
                columns: new[] { "Id", "ChamadoId", "Custo", "DepartamentoId", "Encaminhamentos", "NivelCriticidade", "Observacoes" },
                values: new object[,]
                {
                    { 1, 1, 75.50m, 1, "Direcionamento e encaminhamentos para chamado 1.", "Baixo", "Observações do ticket 1 anexadas no histórico." },
                    { 2, 2, 151.00m, 2, "Direcionamento e encaminhamentos para chamado 2.", "Medio", "Observações do ticket 2 anexadas no histórico." },
                    { 3, 3, 226.50m, 3, "Direcionamento e encaminhamentos para chamado 3.", "Alto", "Observações do ticket 3 anexadas no histórico." },
                    { 4, 4, 302.00m, 4, "Direcionamento e encaminhamentos para chamado 4.", "Critico", "Observações do ticket 4 anexadas no histórico." },
                    { 5, 5, 377.50m, 5, "Direcionamento e encaminhamentos para chamado 5.", "Baixo", "Observações do ticket 5 anexadas no histórico." },
                    { 6, 6, 453.00m, 6, "Direcionamento e encaminhamentos para chamado 6.", "Medio", "Observações do ticket 6 anexadas no histórico." },
                    { 7, 7, null, 7, "Direcionamento e encaminhamentos para chamado 7.", "Alto", "Observações do ticket 7 anexadas no histórico." },
                    { 31, 31, 2340.50m, 3, "Direcionamento e encaminhamentos para chamado 31.", "Alto", "Observações do ticket 31 anexadas no histórico." },
                    { 32, 32, 2416.00m, 4, "Direcionamento e encaminhamentos para chamado 32.", "Critico", "Observações do ticket 32 anexadas no histórico." },
                    { 33, 33, 2491.50m, 5, "Direcionamento e encaminhamentos para chamado 33.", "Baixo", "Observações do ticket 33 anexadas no histórico." },
                    { 34, 34, 2567.00m, 6, "Direcionamento e encaminhamentos para chamado 34.", "Medio", "Observações do ticket 34 anexadas no histórico." },
                    { 35, 35, null, 7, "Direcionamento e encaminhamentos para chamado 35.", "Alto", "Observações do ticket 35 anexadas no histórico." },
                    { 36, 36, 2718.00m, 1, "Direcionamento e encaminhamentos para chamado 36.", "Critico", "Observações do ticket 36 anexadas no histórico." },
                    { 37, 37, 2793.50m, 2, "Direcionamento e encaminhamentos para chamado 37.", "Baixo", "Observações do ticket 37 anexadas no histórico." }
                });

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "Id", "CargoId", "Cpf", "DataContratacao", "DataNascimento", "DepartamentoId", "Email", "Endereco", "Nome", "Perfil", "SenhaHash", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 8, 3, "00000000008", new DateOnly(2023, 9, 9), new DateOnly(1988, 9, 9), 1, "henrique.rodrigues@easydemandas.com", "Rua das Flores, 108", "Henrique", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Rodrigues", "11900000008" },
                    { 9, 4, "00000000009", new DateOnly(2024, 10, 10), new DateOnly(1989, 10, 10), 2, "isabela.martins@easydemandas.com", "Rua das Flores, 109", "Isabela", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Martins", "11900000009" },
                    { 10, 5, "00000000010", new DateOnly(2015, 11, 11), new DateOnly(1990, 11, 11), 3, "joão.alves@easydemandas.com", "Rua das Flores, 110", "João", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Alves", "11900000010" },
                    { 11, 1, "00000000011", new DateOnly(2016, 12, 12), new DateOnly(1991, 12, 12), 4, "julia.almeida@easydemandas.com", "Rua das Flores, 111", "Julia", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Almeida", "11900000011" },
                    { 12, 2, "00000000012", new DateOnly(2017, 1, 13), new DateOnly(1992, 1, 13), 5, "lucas.silva@easydemandas.com", "Rua das Flores, 112", "Lucas", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Silva", "11900000012" },
                    { 13, 3, "00000000013", new DateOnly(2018, 2, 14), new DateOnly(1993, 2, 14), 6, "mariana.barbosa@easydemandas.com", "Rua das Flores, 113", "Mariana", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Barbosa", "11900000013" },
                    { 14, 4, "00000000014", new DateOnly(2019, 3, 15), new DateOnly(1994, 3, 15), 7, "nicolas.soares@easydemandas.com", "Rua das Flores, 114", "Nicolas", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Soares", "11900000014" },
                    { 15, 5, "00000000015", new DateOnly(2020, 4, 16), new DateOnly(1995, 4, 16), 1, "olivia.carvalho@easydemandas.com", "Rua das Flores, 115", "Olivia", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Carvalho", "11900000015" },
                    { 16, 1, "00000000016", new DateOnly(2021, 5, 17), new DateOnly(1996, 5, 17), 2, "pedro.vieira@easydemandas.com", "Rua das Flores, 116", "Pedro", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Vieira", "11900000016" },
                    { 17, 2, "00000000017", new DateOnly(2022, 6, 18), new DateOnly(1997, 6, 18), 3, "sara.ribeiro@easydemandas.com", "Rua das Flores, 117", "Sara", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Ribeiro", "11900000017" },
                    { 18, 3, "00000000018", new DateOnly(2023, 7, 19), new DateOnly(1998, 7, 19), 4, "tiago.gomes@easydemandas.com", "Rua das Flores, 118", "Tiago", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Gomes", "11900000018" },
                    { 19, 4, "00000000019", new DateOnly(2024, 8, 20), new DateOnly(1999, 8, 20), 5, "ursula.fernandes@easydemandas.com", "Rua das Flores, 119", "Ursula", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Fernandes", "11900000019" },
                    { 20, 5, "00000000020", new DateOnly(2015, 9, 21), new DateOnly(1980, 9, 21), 6, "vitor.lopes@easydemandas.com", "Rua das Flores, 120", "Vitor", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Lopes", "11900000020" },
                    { 21, 1, "00000000021", new DateOnly(2016, 10, 22), new DateOnly(1981, 10, 22), 7, "zeca.nascimento@easydemandas.com", "Rua das Flores, 121", "Zeca", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Nascimento", "11900000021" },
                    { 22, 2, "00000000022", new DateOnly(2017, 11, 23), new DateOnly(1982, 11, 23), 1, "fernanda.cardoso@easydemandas.com", "Rua das Flores, 122", "Fernanda", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Cardoso", "11900000022" },
                    { 23, 3, "00000000023", new DateOnly(2018, 12, 24), new DateOnly(1983, 12, 24), 2, "gabriel.teixeira@easydemandas.com", "Rua das Flores, 123", "Gabriel", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Teixeira", "11900000023" },
                    { 24, 4, "00000000024", new DateOnly(2019, 1, 25), new DateOnly(1984, 1, 25), 3, "igor.araujo@easydemandas.com", "Rua das Flores, 124", "Igor", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Araujo", "11900000024" },
                    { 25, 5, "00000000025", new DateOnly(2020, 2, 26), new DateOnly(1985, 2, 26), 4, "diana.melo@easydemandas.com", "Rua das Flores, 125", "Diana", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Melo", "11900000025" },
                    { 26, 1, "00000000026", new DateOnly(2021, 3, 27), new DateOnly(1986, 3, 27), 5, "eduardo.rocha@easydemandas.com", "Rua das Flores, 126", "Eduardo", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Rocha", "11900000026" },
                    { 27, 2, "00000000027", new DateOnly(2022, 4, 28), new DateOnly(1987, 4, 28), 6, "quintino.pinto@easydemandas.com", "Rua das Flores, 127", "Quintino", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Pinto", "11900000027" },
                    { 28, 3, "00000000028", new DateOnly(2023, 5, 1), new DateOnly(1988, 5, 1), 7, "zélia.batista@easydemandas.com", "Rua das Flores, 128", "Zélia", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Batista", "11900000028" },
                    { 29, 4, "00000000029", new DateOnly(2024, 6, 2), new DateOnly(1989, 6, 2), 1, "heitor.montes@easydemandas.com", "Rua das Flores, 129", "Heitor", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Montes", "11900000029" },
                    { 30, 5, "00000000030", new DateOnly(2015, 7, 3), new DateOnly(1990, 7, 3), 2, "clarice.teves@easydemandas.com", "Rua das Flores, 130", "Clarice", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Teves", "11900000030" }
                });

            migrationBuilder.InsertData(
                table: "Chamados",
                columns: new[] { "Id", "DataAbertura", "DataConclusao", "Descricao", "SolicitanteId", "Status", "Titulo" },
                values: new object[,]
                {
                    { 8, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 8 aberto para resolução de acesso ao servidor #8. Favor analisar com atenção.", 8, "Cancelado", "Acesso ao Servidor #8" },
                    { 9, new DateTime(2026, 4, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 9 aberto para resolução de ajuste de permissões #9. Favor analisar com atenção.", 9, "Aberto", "Ajuste de Permissões #9" },
                    { 10, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 10 aberto para resolução de solicitação de upgrade #10. Favor analisar com atenção.", 10, "Em Andamento", "Solicitação de Upgrade #10" },
                    { 11, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 11 aberto para resolução de erro no erp #11. Favor analisar com atenção.", 11, "Concluído", "Erro no ERP #11" },
                    { 12, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 12 aberto para resolução de reset de senha #12. Favor analisar com atenção.", 12, "Cancelado", "Reset de Senha #12" },
                    { 13, new DateTime(2026, 3, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 13 aberto para resolução de problema com vpn #13. Favor analisar com atenção.", 13, "Aberto", "Problema com VPN #13" },
                    { 14, new DateTime(2026, 4, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 14 aberto para resolução de impressora não funciona #14. Favor analisar com atenção.", 14, "Em Andamento", "Impressora Não Funciona #14" },
                    { 15, new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 15 aberto para resolução de lentidão na estação #15. Favor analisar com atenção.", 15, "Concluído", "Lentidão na Estação #15" },
                    { 16, new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 16 aberto para resolução de falha de conectividade #16. Favor analisar com atenção.", 16, "Cancelado", "Falha de Conectividade #16" },
                    { 17, new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 17 aberto para resolução de instalação de office #17. Favor analisar com atenção.", 17, "Aberto", "Instalação de Office #17" },
                    { 18, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 18 aberto para resolução de acesso ao servidor #18. Favor analisar com atenção.", 18, "Em Andamento", "Acesso ao Servidor #18" },
                    { 19, new DateTime(2026, 4, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 19 aberto para resolução de ajuste de permissões #19. Favor analisar com atenção.", 19, "Concluído", "Ajuste de Permissões #19" },
                    { 20, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 20 aberto para resolução de solicitação de upgrade #20. Favor analisar com atenção.", 20, "Cancelado", "Solicitação de Upgrade #20" },
                    { 21, new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 21 aberto para resolução de erro no erp #21. Favor analisar com atenção.", 21, "Aberto", "Erro no ERP #21" },
                    { 22, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 22 aberto para resolução de reset de senha #22. Favor analisar com atenção.", 22, "Em Andamento", "Reset de Senha #22" },
                    { 23, new DateTime(2026, 3, 23, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 23 aberto para resolução de problema com vpn #23. Favor analisar com atenção.", 23, "Concluído", "Problema com VPN #23" },
                    { 24, new DateTime(2026, 4, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 24 aberto para resolução de impressora não funciona #24. Favor analisar com atenção.", 24, "Cancelado", "Impressora Não Funciona #24" },
                    { 25, new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 25 aberto para resolução de lentidão na estação #25. Favor analisar com atenção.", 25, "Aberto", "Lentidão na Estação #25" },
                    { 26, new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 26 aberto para resolução de falha de conectividade #26. Favor analisar com atenção.", 26, "Em Andamento", "Falha de Conectividade #26" },
                    { 27, new DateTime(2026, 2, 27, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 27 aberto para resolução de instalação de office #27. Favor analisar com atenção.", 27, "Concluído", "Instalação de Office #27" },
                    { 28, new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 28 aberto para resolução de acesso ao servidor #28. Favor analisar com atenção.", 28, "Cancelado", "Acesso ao Servidor #28" },
                    { 29, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 29 aberto para resolução de ajuste de permissões #29. Favor analisar com atenção.", 29, "Aberto", "Ajuste de Permissões #29" },
                    { 30, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 30 aberto para resolução de solicitação de upgrade #30. Favor analisar com atenção.", 30, "Em Andamento", "Solicitação de Upgrade #30" },
                    { 38, new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 38 aberto para resolução de acesso ao servidor #38. Favor analisar com atenção.", 8, "Em Andamento", "Acesso ao Servidor #38" },
                    { 39, new DateTime(2026, 4, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 39 aberto para resolução de ajuste de permissões #39. Favor analisar com atenção.", 9, "Concluído", "Ajuste de Permissões #39" },
                    { 40, new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 40 aberto para resolução de solicitação de upgrade #40. Favor analisar com atenção.", 10, "Cancelado", "Solicitação de Upgrade #40" },
                    { 41, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 41 aberto para resolução de erro no erp #41. Favor analisar com atenção.", 11, "Aberto", "Erro no ERP #41" },
                    { 42, new DateTime(2026, 2, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 42 aberto para resolução de reset de senha #42. Favor analisar com atenção.", 12, "Em Andamento", "Reset de Senha #42" },
                    { 43, new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 43 aberto para resolução de problema com vpn #43. Favor analisar com atenção.", 13, "Concluído", "Problema com VPN #43" },
                    { 44, new DateTime(2026, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 44 aberto para resolução de impressora não funciona #44. Favor analisar com atenção.", 14, "Cancelado", "Impressora Não Funciona #44" },
                    { 45, new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 45 aberto para resolução de lentidão na estação #45. Favor analisar com atenção.", 15, "Aberto", "Lentidão na Estação #45" },
                    { 46, new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 46 aberto para resolução de falha de conectividade #46. Favor analisar com atenção.", 16, "Em Andamento", "Falha de Conectividade #46" },
                    { 47, new DateTime(2026, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 47 aberto para resolução de instalação de office #47. Favor analisar com atenção.", 17, "Concluído", "Instalação de Office #47" },
                    { 48, new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 48 aberto para resolução de acesso ao servidor #48. Favor analisar com atenção.", 18, "Cancelado", "Acesso ao Servidor #48" },
                    { 49, new DateTime(2026, 4, 21, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 49 aberto para resolução de ajuste de permissões #49. Favor analisar com atenção.", 19, "Aberto", "Ajuste de Permissões #49" },
                    { 50, new DateTime(2026, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chamado número 50 aberto para resolução de solicitação de upgrade #50. Favor analisar com atenção.", 20, "Em Andamento", "Solicitação de Upgrade #50" }
                });

            migrationBuilder.InsertData(
                table: "DetalhesChamados",
                columns: new[] { "Id", "ChamadoId", "Custo", "DepartamentoId", "Encaminhamentos", "NivelCriticidade", "Observacoes" },
                values: new object[,]
                {
                    { 8, 8, 604.00m, 1, "Direcionamento e encaminhamentos para chamado 8.", "Critico", "Observações do ticket 8 anexadas no histórico." },
                    { 9, 9, 679.50m, 2, "Direcionamento e encaminhamentos para chamado 9.", "Baixo", "Observações do ticket 9 anexadas no histórico." },
                    { 10, 10, 755.00m, 3, "Direcionamento e encaminhamentos para chamado 10.", "Medio", "Observações do ticket 10 anexadas no histórico." },
                    { 11, 11, 830.50m, 4, "Direcionamento e encaminhamentos para chamado 11.", "Alto", "Observações do ticket 11 anexadas no histórico." },
                    { 12, 12, 906.00m, 5, "Direcionamento e encaminhamentos para chamado 12.", "Critico", "Observações do ticket 12 anexadas no histórico." },
                    { 13, 13, 981.50m, 6, "Direcionamento e encaminhamentos para chamado 13.", "Baixo", "Observações do ticket 13 anexadas no histórico." },
                    { 14, 14, null, 7, "Direcionamento e encaminhamentos para chamado 14.", "Medio", "Observações do ticket 14 anexadas no histórico." },
                    { 15, 15, 1132.50m, 1, "Direcionamento e encaminhamentos para chamado 15.", "Alto", "Observações do ticket 15 anexadas no histórico." },
                    { 16, 16, 1208.00m, 2, "Direcionamento e encaminhamentos para chamado 16.", "Critico", "Observações do ticket 16 anexadas no histórico." },
                    { 17, 17, 1283.50m, 3, "Direcionamento e encaminhamentos para chamado 17.", "Baixo", "Observações do ticket 17 anexadas no histórico." },
                    { 18, 18, 1359.00m, 4, "Direcionamento e encaminhamentos para chamado 18.", "Medio", "Observações do ticket 18 anexadas no histórico." },
                    { 19, 19, 1434.50m, 5, "Direcionamento e encaminhamentos para chamado 19.", "Alto", "Observações do ticket 19 anexadas no histórico." },
                    { 20, 20, 1510.00m, 6, "Direcionamento e encaminhamentos para chamado 20.", "Critico", "Observações do ticket 20 anexadas no histórico." },
                    { 21, 21, null, 7, "Direcionamento e encaminhamentos para chamado 21.", "Baixo", "Observações do ticket 21 anexadas no histórico." },
                    { 22, 22, 1661.00m, 1, "Direcionamento e encaminhamentos para chamado 22.", "Medio", "Observações do ticket 22 anexadas no histórico." },
                    { 23, 23, 1736.50m, 2, "Direcionamento e encaminhamentos para chamado 23.", "Alto", "Observações do ticket 23 anexadas no histórico." },
                    { 24, 24, 1812.00m, 3, "Direcionamento e encaminhamentos para chamado 24.", "Critico", "Observações do ticket 24 anexadas no histórico." },
                    { 25, 25, 1887.50m, 4, "Direcionamento e encaminhamentos para chamado 25.", "Baixo", "Observações do ticket 25 anexadas no histórico." },
                    { 26, 26, 1963.00m, 5, "Direcionamento e encaminhamentos para chamado 26.", "Medio", "Observações do ticket 26 anexadas no histórico." },
                    { 27, 27, 2038.50m, 6, "Direcionamento e encaminhamentos para chamado 27.", "Alto", "Observações do ticket 27 anexadas no histórico." },
                    { 28, 28, null, 7, "Direcionamento e encaminhamentos para chamado 28.", "Critico", "Observações do ticket 28 anexadas no histórico." },
                    { 29, 29, 2189.50m, 1, "Direcionamento e encaminhamentos para chamado 29.", "Baixo", "Observações do ticket 29 anexadas no histórico." },
                    { 30, 30, 2265.00m, 2, "Direcionamento e encaminhamentos para chamado 30.", "Medio", "Observações do ticket 30 anexadas no histórico." },
                    { 38, 38, 2869.00m, 3, "Direcionamento e encaminhamentos para chamado 38.", "Medio", "Observações do ticket 38 anexadas no histórico." },
                    { 39, 39, 2944.50m, 4, "Direcionamento e encaminhamentos para chamado 39.", "Alto", "Observações do ticket 39 anexadas no histórico." },
                    { 40, 40, 3020.00m, 5, "Direcionamento e encaminhamentos para chamado 40.", "Critico", "Observações do ticket 40 anexadas no histórico." },
                    { 41, 41, 3095.50m, 6, "Direcionamento e encaminhamentos para chamado 41.", "Baixo", "Observações do ticket 41 anexadas no histórico." },
                    { 42, 42, null, 7, "Direcionamento e encaminhamentos para chamado 42.", "Medio", "Observações do ticket 42 anexadas no histórico." },
                    { 43, 43, 3246.50m, 1, "Direcionamento e encaminhamentos para chamado 43.", "Alto", "Observações do ticket 43 anexadas no histórico." },
                    { 44, 44, 3322.00m, 2, "Direcionamento e encaminhamentos para chamado 44.", "Critico", "Observações do ticket 44 anexadas no histórico." },
                    { 45, 45, 3397.50m, 3, "Direcionamento e encaminhamentos para chamado 45.", "Baixo", "Observações do ticket 45 anexadas no histórico." },
                    { 46, 46, 3473.00m, 4, "Direcionamento e encaminhamentos para chamado 46.", "Medio", "Observações do ticket 46 anexadas no histórico." },
                    { 47, 47, 3548.50m, 5, "Direcionamento e encaminhamentos para chamado 47.", "Alto", "Observações do ticket 47 anexadas no histórico." },
                    { 48, 48, 3624.00m, 6, "Direcionamento e encaminhamentos para chamado 48.", "Critico", "Observações do ticket 48 anexadas no histórico." },
                    { 49, 49, null, 7, "Direcionamento e encaminhamentos para chamado 49.", "Baixo", "Observações do ticket 49 anexadas no histórico." },
                    { 50, 50, 3775.00m, 1, "Direcionamento e encaminhamentos para chamado 50.", "Medio", "Observações do ticket 50 anexadas no histórico." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "DetalhesChamados",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Chamados",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Empregados",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Empregados",
                columns: new[] { "Id", "CargoId", "Cpf", "DataContratacao", "DataNascimento", "DepartamentoId", "Email", "Endereco", "Nome", "Perfil", "SenhaHash", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 9001, 9999, "11111111111", new DateOnly(2020, 1, 1), new DateOnly(1985, 5, 10), null, "gestor@easydemandas.com", "Rua do Gestor, 123", "Gestor", "Gestor", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Plataforma", "11999999999" },
                    { 9002, 9999, "22222222222", new DateOnly(2021, 2, 1), new DateOnly(1990, 6, 15), null, "rh@easydemandas.com", "Rua do RH, 123", "RH", "RH", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Usuario", "11888888888" },
                    { 9003, 9999, "33333333333", new DateOnly(2022, 3, 1), new DateOnly(1992, 8, 20), null, "suporte@easydemandas.com", "Rua do Suporte, 123", "Suporte", "Suporte", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Chamados", "11777777777" },
                    { 9004, 9999, "44444444444", new DateOnly(2023, 4, 1), new DateOnly(1995, 10, 25), null, "usuario@easydemandas.com", "Rua do Usuario, 123", "Usuario", "Usuario", "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "Comum", "11666666666" }
                });
        }
    }
}
