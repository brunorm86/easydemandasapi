using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace easydemandasapi.Migrations
{
    public partial class SeedMoreData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 5 novos Cargos (IDs 6-10)
            migrationBuilder.Sql(@"
                INSERT INTO ""Cargos"" (""Id"",""Nome"") VALUES
                (6, 'Diretor de TI'),
                (7, 'Assistente Administrativo'),
                (8, 'Arquiteto de Software'),
                (9, 'Controller Financeiro'),
                (10,'Especialista em RH')
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            migrationBuilder.Sql(@"
                DO $$ DECLARE n INT;
                BEGIN SELECT COALESCE(MAX(""Id""),0)+1 INTO n FROM ""Cargos"";
                EXECUTE 'ALTER TABLE ""Cargos"" ALTER COLUMN ""Id"" RESTART WITH '||n; END $$;
            ");

            // 10 novos Empregados (IDs 11-20) — DepartamentoId NULL por enquanto
            migrationBuilder.Sql(@"
                INSERT INTO ""Empregados"" (""Id"",""Nome"",""Sobrenome"",""Email"",""Telefone"",""Endereco"",""Cpf"",""DataNascimento"",""CargoId"",""DataContratacao"",""DepartamentoId"") VALUES
                (11,'Marcos',   'Almeida',  'marcos.almeida@empresa.com',   '11992001001','Rua das Palmeiras, 11, SP',    '11100011100','1980-04-12',6, '2010-03-01',NULL),
                (12,'Patricia', 'Ribeiro',  'patricia.ribeiro@empresa.com', '11992002002','Av. Morumbi, 12, SP',          '22200022200','1986-09-25',7, '2016-08-10',NULL),
                (13,'Ricardo',  'Teixeira', 'ricardo.teixeira@empresa.com', '11992003003','Rua do Sol, 13, Campinas',     '33300033300','1983-11-30',8, '2013-05-20',NULL),
                (14,'Sandra',   'Barbosa',  'sandra.barbosa@empresa.com',   '11992004004','Av. Central, 14, Curitiba',    '44400044400','1991-07-15',9, '2021-02-28',NULL),
                (15,'Thiago',   'Campos',   'thiago.campos@empresa.com',    '11992005005','Rua Nova, 15, BH',             '55500055500','1988-01-08',10,'2018-11-05',NULL),
                (16,'Renata',   'Fonseca',  'renata.fonseca@empresa.com',   '11992006006','Av. Brasil, 16, Recife',       '66600066600','1994-06-22',1, '2023-01-15',NULL),
                (17,'Eduardo',  'Pinto',    'eduardo.pinto@empresa.com',    '11992007007','Rua XV, 17, Florianópolis',    '77700077700','1987-03-18',3, '2015-07-01',NULL),
                (18,'Fernanda', 'Dias',     'fernanda.dias@empresa.com',    '11992008008','Av. Atlântica, 18, RJ',        '88800088800','1992-10-05',4, '2020-09-10',NULL),
                (19,'Gustavo',  'Mendes',   'gustavo.mendes@empresa.com',   '11992009009','Rua do Lago, 19, Goiânia',     '99900099900','1979-12-01',2, '2008-04-22',NULL),
                (20,'Luciana',  'Castro',   'luciana.castro@empresa.com',   '11992010010','Av. das Flores, 20, Porto Alegre','00000000001','1990-08-14',5,'2019-06-30',NULL)
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            migrationBuilder.Sql(@"
                DO $$ DECLARE n INT;
                BEGIN SELECT COALESCE(MAX(""Id""),0)+1 INTO n FROM ""Empregados"";
                EXECUTE 'ALTER TABLE ""Empregados"" ALTER COLUMN ""Id"" RESTART WITH '||n; END $$;
            ");

            // 5 novos Departamentos (IDs 5-9) — responsáveis nos empregados 11-15
            migrationBuilder.Sql(@"
                INSERT INTO ""Departamentos"" (""Id"",""Nome"",""Sigla"",""ResponsavelId"") VALUES
                (5,'Jurídico',        'JUR',11),
                (6,'Marketing',       'MKT',12),
                (7,'Compras',         'COM',13),
                (8,'Qualidade',       'QUA',14),
                (9,'Administrativo',  'ADM',15)
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            migrationBuilder.Sql(@"
                DO $$ DECLARE n INT;
                BEGIN SELECT COALESCE(MAX(""Id""),0)+1 INTO n FROM ""Departamentos"";
                EXECUTE 'ALTER TABLE ""Departamentos"" ALTER COLUMN ""Id"" RESTART WITH '||n; END $$;
            ");

            // Atualiza departamentos dos novos empregados
            migrationBuilder.Sql(@"
                UPDATE ""Empregados"" SET ""DepartamentoId""=5 WHERE ""Id"" IN (11,16) AND ""DepartamentoId"" IS NULL;
                UPDATE ""Empregados"" SET ""DepartamentoId""=6 WHERE ""Id"" IN (12,17) AND ""DepartamentoId"" IS NULL;
                UPDATE ""Empregados"" SET ""DepartamentoId""=7 WHERE ""Id"" IN (13,18) AND ""DepartamentoId"" IS NULL;
                UPDATE ""Empregados"" SET ""DepartamentoId""=8 WHERE ""Id"" IN (14,19) AND ""DepartamentoId"" IS NULL;
                UPDATE ""Empregados"" SET ""DepartamentoId""=9 WHERE ""Id"" IN (15,20) AND ""DepartamentoId"" IS NULL;
            ");

            // 30 novos Chamados (IDs 41-70)
            migrationBuilder.Sql(@"
                INSERT INTO ""Chamados"" (""Id"",""Titulo"",""Descricao"",""Status"",""DataAbertura"",""SolicitanteId"") VALUES
                (41,'Contrato sem assinatura digital',   'Contrato aguardando assinatura eletrônica.', 'Aberto',      '2026-04-10',11),
                (42,'Campanha de marketing parada',      'Ferramenta de automação fora do ar.',        'Aberto',      '2026-04-11',12),
                (43,'Pedido de compra duplicado',        'Pedido criado duas vezes no sistema.',       'Aberto',      '2026-04-12',13),
                (44,'Auditoria de qualidade pendente',   'Checklist de qualidade não disponível.',     'Aberto',      '2026-04-13',14),
                (45,'Acesso ao portal RH negado',        'Colaborador sem acesso ao portal.',          'Aberto',      '2026-04-14',15),
                (46,'Nota fiscal rejeitada',             'NF rejeitada pela SEFAZ.',                   'Aberto',      '2026-04-15',16),
                (47,'Projetor da sala sem imagem',       'Projetor da sala de reuniões sem sinal.',    'Aberto',      '2026-04-16',17),
                (48,'Certificado SSL vencido',           'Certificado do site vencido.',               'Aberto',      '2026-04-17',18),
                (49,'Integração EDI com falha',          'Mensagens EDI não estão chegando.',          'Aberto',      '2026-04-18',19),
                (50,'Relatório de vendas incorreto',     'Valores do relatório não batem.',            'Aberto',      '2026-04-19',20),

                (51,'Atualização do ERP',                'Atualizar ERP para versão 5.2.',             'Em Andamento','2026-03-12',11),
                (52,'Criação de landing page',           'Desenvolver LP para campanha.',              'Em Andamento','2026-03-13',12),
                (53,'Homologação de fornecedor',         'Homologar novo fornecedor de peças.',        'Em Andamento','2026-03-14',13),
                (54,'Implantação de ISO 9001',           'Preparar documentação para certificação.',   'Em Andamento','2026-03-15',14),
                (55,'Reestruturação do arquivo físico',  'Reorganizar documentos do departamento.',    'Em Andamento','2026-03-16',15),
                (56,'Implantação de BI',                 'Conectar Power BI ao banco de dados.',       'Em Andamento','2026-03-17',16),
                (57,'Substituição de switch de rede',    'Switch do andar 2 com falha intermitente.',  'Em Andamento','2026-03-18',17),
                (58,'Revisão de política de privacidade','Adequar política à LGPD.',                   'Em Andamento','2026-03-19',18),
                (59,'Atualização de catálogo de compras','Atualizar preços no sistema.',               'Em Andamento','2026-03-20',19),
                (60,'Implantação de CRM',                'Configurar CRM para equipe comercial.',      'Em Andamento','2026-03-21',20),

                (61,'Revisão de cláusula contratual',    'Revisar cláusula de rescisão de contrato.',  'Concluído',   '2026-02-10',11),
                (62,'Criação de identidade visual',      'Criar manual de marca da empresa.',          'Concluído',   '2026-02-11',12),
                (63,'Negociação com fornecedor',         'Reduzir custo de insumos em 15%.',           'Concluído',   '2026-02-12',13),
                (64,'Treinamento de qualidade',          'Capacitar equipe em normas ISO.',            'Concluído',   '2026-02-13',14),
                (65,'Digitalização de documentos',       'Digitalizar contratos de 2020 a 2022.',      'Concluído',   '2026-02-14',15),
                (66,'Relatório de conformidade LGPD',    'Elaborar relatório anual de conformidade.',  'Concluído',   '2026-02-15',16),
                (67,'Expansão da rede cabeada',          'Cabeamento estruturado no andar 4.',         'Concluído',   '2026-02-16',17),
                (68,'Revisão de processos internos',     'Mapear e otimizar fluxos do departamento.',  'Concluído',   '2026-02-17',18),
                (69,'Licitação de materiais de escritório','Conduzir processo de licitação.',          'Concluído',   '2026-02-18',19),
                (70,'Pesquisa de satisfação interna',    'Aplicar pesquisa de clima organizacional.',  'Concluído',   '2026-02-19',20)
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            migrationBuilder.Sql(@"
                DO $$ DECLARE n INT;
                BEGIN SELECT COALESCE(MAX(""Id""),0)+1 INTO n FROM ""Chamados"";
                EXECUTE 'ALTER TABLE ""Chamados"" ALTER COLUMN ""Id"" RESTART WITH '||n; END $$;
            ");

            // DetalhesChamados para os 30 novos chamados (IDs 41-70)
            migrationBuilder.Sql(@"
                INSERT INTO ""DetalhesChamados"" (""Id"",""ChamadoId"",""DepartamentoId"",""Custo"",""NivelCriticidade"",""Observacoes"",""Encaminhamentos"") VALUES
                (41,41,5,   0.00,'Medio', 'Aguardando assinatura do cliente.',         'Enviar lembrete.'),
                (42,42,6,1200.00,'Alto',  'Ferramenta de automação sem resposta.',     'Contatar suporte do fornecedor.'),
                (43,43,7, 500.00,'Medio', 'Pedido duplicado no módulo de compras.',    'Cancelar o pedido duplicado.'),
                (44,44,8,   0.00,'Baixo', 'Sistema de checklist indisponível.',        'Usar planilha temporária.'),
                (45,45,2,   0.00,'Baixo', 'Portal RH fora do ar para o usuário.',     'Recriar permissão.'),
                (46,46,3,3200.00,'Alto',  'SEFAZ retornou código de rejeição 999.',   'Acionar contador.'),
                (47,47,1, 800.00,'Baixo', 'Cabo HDMI com defeito.',                   'Substituir cabo.'),
                (48,48,1,1500.00,'Critico','Site com alerta de segurança no browser.','Renovar certificado imediatamente.'),
                (49,49,7,2800.00,'Alto',  'Parceiro EDI sem conexão há 2 dias.',      'Abrir chamado no parceiro.'),
                (50,50,3,   0.00,'Medio', 'Filtro de data com bug no relatório.',     'Acionar equipe de BI.'),
                (51,51,1,42000.00,'Alto', 'Versão 5.2 em ambiente de homologação.',   'Migração prevista para sexta.'),
                (52,52,6, 8500.00,'Baixo','Briefing aprovado pela diretoria.',        'Design em andamento.'),
                (53,53,7, 3000.00,'Medio','Documentação enviada ao fornecedor.',      'Aguardando retorno.'),
                (54,54,8,12000.00,'Medio','Auditoria interna realizada.',             'Auditoria externa agendada.'),
                (55,55,9,   0.00,'Baixo', 'Digitalização de 30% dos documentos.',    'Continuidade na próxima semana.'),
                (56,56,1,18000.00,'Medio','Power BI conectado ao banco de dev.',      'Homologar com dados reais.'),
                (57,57,1, 4500.00,'Alto', 'Switch substituto em pedido.',             'Chegada prevista: 5 dias.'),
                (58,58,5, 5000.00,'Alto', 'Análise jurídica 80% concluída.',         'Publicação prevista para próximo mês.'),
                (59,59,7,   0.00,'Baixo', '60% dos itens atualizados.',              'Concluir até sexta.'),
                (60,60,6,22000.00,'Medio','CRM instalado em ambiente de teste.',      'Treinamento agendado.'),
                (61,61,5, 2000.00,'Medio','Cláusula revisada e aprovada.',           'Contrato reencaminhado.'),
                (62,62,6, 9500.00,'Baixo','Manual de marca entregue.',               'Aprovado pela diretoria.'),
                (63,63,7, 6000.00,'Baixo','Economia de R$28.000 no semestre.',       'Contrato assinado.'),
                (64,64,8, 3500.00,'Baixo','45 colaboradores certificados.',          'Nota média: 8,7.'),
                (65,65,9,   0.00,'Baixo', '3.200 documentos digitalizados.',         'Indexação concluída.'),
                (66,66,5, 4000.00,'Medio','Relatório enviado ao DPO.',              'Aprovado sem ressalvas.'),
                (67,67,1,28000.00,'Medio','400m de cabos instalados.',              'Rede validada pela TI.'),
                (68,68,9, 2500.00,'Baixo','10 processos mapeados e otimizados.',     'Redução de 20% no tempo médio.'),
                (69,69,7, 1800.00,'Baixo','3 fornecedores homologados.',            'Economia de 12%.'),
                (70,70,2, 1200.00,'Baixo','850 respostas coletadas.',               'Relatório entregue à diretoria.')
                ON CONFLICT (""Id"") DO NOTHING;
            ");
            migrationBuilder.Sql(@"
                DO $$ DECLARE n INT;
                BEGIN SELECT COALESCE(MAX(""Id""),0)+1 INTO n FROM ""DetalhesChamados"";
                EXECUTE 'ALTER TABLE ""DetalhesChamados"" ALTER COLUMN ""Id"" RESTART WITH '||n; END $$;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM ""DetalhesChamados"" WHERE ""Id"" BETWEEN 41 AND 70;
                DELETE FROM ""Chamados""          WHERE ""Id"" BETWEEN 41 AND 70;
                UPDATE ""Empregados"" SET ""DepartamentoId""=NULL WHERE ""Id"" BETWEEN 11 AND 20;
                DELETE FROM ""Departamentos""     WHERE ""Id"" BETWEEN 5 AND 9;
                DELETE FROM ""Empregados""        WHERE ""Id"" BETWEEN 11 AND 20;
                DELETE FROM ""Cargos""            WHERE ""Id"" BETWEEN 6 AND 10;
            ");
        }
    }
}
