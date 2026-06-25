# Roteiro de Apresentação Definitivo: Avaliação do Projeto EasyDemandas

Este documento contém um roteiro detalhado e expansivo para a apresentação do projeto **EasyDemandas** em avaliações acadêmicas de graduação (TCC, Projetos Integradores ou Bancas Avaliadoras). O foco é demonstrar domínio irrestrito sobre arquitetura de software, engenharia de dados, regras de negócio e stack tecnológico (.NET + React).

---

## PARTE 1: Visão Geral, Problema e Solução (O Negócio)

*Dica de oratória: Comece contextualizando o problema do mundo real antes de falar de código.*

### 1.1. Contextualização e o Problema
- **O Cenário Atual:** Muitas empresas de médio porte ainda gerenciam suas demandas internas (TI, Manutenção, RH) por e-mail, mensagens de WhatsApp ou planilhas descentralizadas.
- **O Problema (As Dores):** Isso gera perda de histórico, falta de visibilidade sobre o volume de trabalho de cada setor, atrasos não quantificados e impossibilidade de extrair métricas de desempenho para tomada de decisão (ex: "Qual setor mais abre chamados? Onde estão os gargalos?").

### 1.2. A Solução: EasyDemandas
- **O que é o sistema:** Uma plataforma web centralizada para abertura, acompanhamento e gestão de chamados de serviços internos.
- **Público-alvo e Perfis de Usuário:**
  - **Solicitantes (Funcionários base):** Abrem as demandas e acompanham o status.
  - **Atendentes/Técnicos:** Recebem, processam e resolvem os chamados.
  - **Gestores/Diretores:** Consomem o **Dashboard Gerencial** para analisar KPIs operacionais em tempo real e embasar decisões estratégicas (como contratação de mais pessoal para um setor sobrecarregado).

---

## PARTE 2: Arquitetura e Stack Tecnológico (O "Como")

*Dica de oratória: Demonstre que cada tecnologia foi escolhida por um motivo, não por acaso.*

### 2.1. Backend (O Motor do Sistema)
- **Framework:** **ASP.NET Core (C#)**. Escolhido pela sua altíssima performance, robustez no ambiente corporativo, tipagem forte (que previne erros em tempo de compilação) e excelente suporte a Injeção de Dependência nativa.
- **Arquitetura de API:** Padrão **RESTful**, expondo endpoints estruturados (`GET`, `POST`, `PUT`, `DELETE`) que respondem em formato JSON.
- **ORM (Mapeamento Objeto-Relacional):** **Entity Framework Core**. Abstrai a complexidade do banco de dados e permite gerenciar o esquema via código (Code-First Migrations).

### 2.2. Banco de Dados (Persistência)
- **SGBD:** **PostgreSQL** conectado via driver `Npgsql`. Escolhido por ser open-source, ter conformidade ACID rigorosa, excelente performance com concorrência e suportar recursos avançados (JSONB, Arrays).

### 2.3. Frontend (Interface do Usuário)
- **Biblioteca Base:** **React (JavaScript/JSX)**. Permite a criação de interfaces ricas, reativas (Single Page Application - SPA) e altamente componentizadas.
- **Visualização de Dados:** Biblioteca **Recharts** para construção dos gráficos do Dashboard de forma declarativa e integrada ao ecossistema React.
- **Comunicação de Rede:** Chamadas assíncronas utilizando a API nativa `Fetch` (ou `Axios`), consumindo a API REST.

---

## PARTE 3: Deep Dive no Código Backend (Mostrando Domínio)

*Nesta etapa, abra a IDE (Visual Studio / VS Code) e explique os 4 pilares técnicos do projeto.*

### 3.1. O Ponto de Partida (`Program.cs`)
**Objetivo:** Demonstrar conhecimento sobre o ciclo de vida da aplicação, Middlewares e Injeção de Dependência.
- **Injeção de Dependência (DI):** Mostre o `builder.Services.AddDbContext()`. Explique que instanciar classes com `new` gera acoplamento forte. A DI permite que o ASP.NET gerencie o ciclo de vida das classes (como o contexto do banco), facilitando testes unitários e trocas de implementação.
- **Tratamento de Ciclos de Referência (`ReferenceHandler.IgnoreCycles`):** Explique o problema clássico de serialização: um *Departamento* tem *Empregados*, e cada *Empregado* pertence a um *Departamento*. Se o serializador JSON tentar converter isso, entrará em loop infinito. Essa configuração previne o travamento.
- **CORS (Cross-Origin Resource Sharing):** Explique por que a API precisa do `builder.Services.AddCors()`. Como o React roda na porta 3000 e a API na porta 5000, o navegador bloqueia a requisição por segurança. A política de CORS configurada diz ao navegador: "Confie nesta origem".
- **Migrations Automáticas no Startup:** Mostre a linha `context.Database.Migrate()`. Explique que, em um ambiente DevOps/Cloud, não queremos rodar comandos de terminal manualmente. A aplicação avalia seu próprio banco ao iniciar e se atualiza sozinha.

### 3.2. Modelagem e Regras de Integridade (`AppDbContext.cs` e Fluent API)
**Objetivo:** Mostrar domínio sobre engenharia de dados e restrições (Constraints).
- **Comportamento de Exclusão (Delete Behavior):**
  - Mostre onde usou `DeleteBehavior.Restrict`. *Exemplo prático:* "O banco impede a exclusão de um Departamento se ele ainda tiver Empregados ativos. Isso garante integridade histórica (não teremos chamados apontando para um departamento fantasma)."
  - Mostre `DeleteBehavior.Cascade`. *Exemplo prático:* "Um `Chamado` e seus `Detalhes`. Se o chamado for cancelado ou apagado definitivamente, os anexos/detalhes não têm mais serventia, logo, o banco os limpa automaticamente."
- **Padrão Fallback/Indeterminado (Seed de ID 9999):** Estratégia de consistência onde relacionamentos ausentes ou deletados logicamente caem para um registro "Indeterminado", evitando o uso excessivo de colunas `NULL` que prejudicariam sumarizações em gráficos.

### 3.3. Controladores e Performance (`DashboardController.cs`)
**Objetivo:** Demonstrar otimização de consultas e separação de conceitos.
- **O Padrão DTO (Data Transfer Object):** Mostre que os métodos retornam classes como `DashboardChamadosDto` em vez dos Models do EF Core. Justifique que DTOs protegem a estrutura do banco de dados (segurança) e enviam pela rede apenas o tráfego estritamente necessário para preencher a tela, economizando banda.
- **O Problema N+1 e o Eager Loading:** 
  - *Explicação matadora:* "Se eu buscar 100 chamados do banco, e o Entity Framework precisar do nome do Empregado de cada chamado na hora de montar o JSON, ele fará 1 query inicial + 100 queries secundárias de consulta (O temido Select N+1). Usando `.Include()` e `.ThenInclude()`, eu forço o banco de dados a realizar um `JOIN` massivo, resolvendo tudo em uma única viagem ao servidor, reduzindo a latência a quase zero."
- **LINQ para Agregações Complexas:** Demonstre o uso de `.Where()`, `.GroupBy()` e `.Select()` executando diretamente em memória ou traduzidos para SQL para montar as estatísticas dos gráficos de maneira elegante e livre de laços `for/foreach` macarrônicos.

### 3.4. Carga de Dados Robusta (`SeedDemoData.cs` / Migrations Customizadas)
**Objetivo:** Mostrar proficiência em SQL e conhecimento íntimo do PostgreSQL.
- **Uso de SQL Bruto nas Migrations (`migrationBuilder.Sql`):** Inserir centenas de registros para popular gráficos via código C# `HasData` é limitado. O script SQL inserido diretamente na Migration permite carga massiva de cenários reais instantaneamente.
- **Correção Dinâmica de Sequence (IDENTITY):**
  - *Explicação matadora:* "Quando inserimos IDs manualmente por SQL (ex: Forçar chamados do ID 1 ao 50), a *Sequence* interna do PostgreSQL não acompanha. Se tentássemos abrir um chamado novo pela interface gráfica, o banco tentaria usar o ID 1 e daria erro de chave primária duplicada. Implementamos um bloco `DO $$ DECLARE` em PL/pgSQL que recalcula a sequence dinamicamente com base no valor máximo atual inserido. Isso mostra domínio sobre a engine do banco."

---

## PARTE 4: Experiência do Usuário e Frontend (React)

*Abra o VS Code do Frontend ou os DevTools do navegador.*

### 4.1. Gerenciamento de Estado e Ciclo de Vida
- **`useState` e `useEffect`:** Explique como o componente do Dashboard não é estático. Ao montar (`useEffect`), ele dispara a requisição `fetch` para a API.
- **UX de Carregamento:** Durante a chamada de rede, o estado `isLoading` exibe um spinner (loading). Se houver erro de rede (API offline), cai no estado de erro amigável (tratamento de exceção). Isso previne a tela em branco.

### 4.2. Componentização
- Mostre que a tela não é um arquivo gigante de 1000 linhas. Foram criados componentes como `<KpiCard />` e `<BarChartComponent />` que recebem propriedades (`props`). Isso torna o código manutenível e reaproveitável.

### 4.3. Renderização Gráfica Integrada
- Mostre como o JSON retornado pela API via `LabelValueDto` foi projetado de forma idêntica à estrutura que a biblioteca **Recharts** exige. O React não precisa fazer manipulações pesadas de vetores no navegador; ele simplesmente repassa o JSON da API direto para o gráfico.

---

## PARTE 5: Demonstração Prática (Showtime)

1. Mostre a aplicação rodando.
2. Acesse a interface do **Swagger** (`/swagger`). Faça uma requisição GET ao vivo demonstrando a resposta em 200 OK com o JSON limpo (graças aos DTOs).
3. Abra a interface React. Interaja com os gráficos, mostre os *tooltips* (caixas de detalhe ao passar o mouse).
4. **Fechamento:** "Senhores avaliadores, este projeto não foi construído apenas para ser funcional, mas para seguir práticas de mercado: ele é **escalável** (separação front/back), **performático** (queries otimizadas) e **resiliente** (integridade no banco e tratamento de dependências cíclicas)."

---

## PARTE 6: FAQ - Perguntas Frequentes da Banca (Arguição)

Esta seção foi montada especificamente para prever e armar você com respostas técnicas aprofundadas para as perguntas mais capciosas que bancas costumam fazer.

### 6.1. Sobre Arquitetura e Padrões

**Q1: Por que vocês optaram por uma arquitetura separada (React + Web API REST) em vez de usar um modelo monolítico tradicional como o ASP.NET MVC com Razor Pages?**
> **Resposta Esperada:** "Optamos pelo desacoplamento para garantir escalabilidade e flexibilidade. Uma arquitetura baseada em API REST permite que, no futuro, possamos desenvolver um aplicativo mobile (em React Native ou Flutter) sem precisar reescrever a lógica de negócio ou as consultas ao banco; basta consumir os mesmos endpoints. Além disso, permite que a equipe de Front-end e Back-end trabalhem de forma assíncrona."

**Q2: Se o sistema crescer, por que não usaram Microsserviços em vez desse monólito de API?**
> **Resposta Esperada:** "A arquitetura de microsserviços traz um overhead enorme de infraestrutura (Kubernetes, mensageria via RabbitMQ/Kafka, latência de rede). Para o escopo do EasyDemandas e seu estágio inicial, um monólito modular bem escrito (com forte coesão e baixo acoplamento interno) é a escolha pragmática, mais fácil de manter e de realizar deploy. O software precisa nascer simples para evoluir."

**Q3: Por que criar classes DTOs (Data Transfer Objects)? Não é retrabalho já que vocês têm as entidades do banco?**
> **Resposta Esperada:** "Retornar Entidades de banco de dados diretamente em endpoints HTTP é um *anti-pattern* (má prática). Isso gera três problemas: 1) Vazamento de informações da estrutura do banco para o mundo externo (falha de segurança); 2) Riscos de `Over-posting` em verbos PUT/POST; 3) Serialização de relacionamentos não desejados (gerando ciclos de referência ou dados muito pesados). O DTO funciona como um contrato de API focado no que a tela realmente precisa, reduzindo a carga (*payload*) trafegada na rede."

### 6.2. Sobre o Banco de Dados e EF Core

**Q4: O que aconteceria se um usuário do sistema tentasse excluir o 'Departamento de TI', que já possui vários chamados vinculados?**
> **Resposta Esperada:** "Isso não é possível de forma acidental. Nós configuramos explicitamente as regras no Fluent API do Entity Framework usando `DeleteBehavior.Restrict` (ou proteção natural do banco com chaves estrangeiras). O banco de dados lançará uma exceção de *Constraint Violation*. Isso é vital para garantir que chamados históricos não fiquem órfãos, preservando métricas e relatórios antigos. A melhor forma de 'apagar' o departamento seria a Inativação Lógica (Soft Delete)."

**Q5: Explique o problema das "N+1 Queries". Como vocês garantiram que o Dashboard não vai sobrecarregar o banco de dados?**
> **Resposta Esperada:** "O problema N+1 ocorre quando um ORM faz uma query para retornar N registros e, em seguida, dispara N queries individuais em loop para buscar relacionamentos de cada registro. Nós neutralizamos isso usando **Eager Loading** com os comandos `.Include()` e `.ThenInclude()` do Entity Framework Core. Dessa forma, o EF Core constrói um `LEFT JOIN` ou `INNER JOIN` nativo e vai ao servidor do PostgreSQL apenas uma vez, trazendo toda a árvore de dados otimizada."

**Q6: Por que escolheram PostgreSQL e não o SQL Server ou MySQL?**
> **Resposta Esperada:** "O PostgreSQL é amplamente reconhecido pela sua robustez, conformidade total com os preceitos ACID, e excelente performance com alto grau de concorrência. Além de ser open-source (livre de altos custos de licenciamento comercial corporativo), o provider `Npgsql` do Entity Framework Core possui uma das melhores e mais atualizadas integrações da comunidade .NET."

**Q7: Eu notei que há scripts SQL puros rodando nas Migrations (Seed de Dados). Por que vocês misturaram C# com SQL bruto?**
> **Resposta Esperada:** "Embora o EF Core possua o método `.HasData()` para criar dados iniciais puramente em C#, ele se torna ineficiente para volumes altos e relacionamentos complexos, gerando código muito sujo. O SQL bruto dentro da migration foi usado como script de *bootstrap* (carga inicial real) para preencher gráficos de maneira pragmática. Além disso, ele nos permitiu ajustar a Sequence dinamicamente (comando `DO $$ DECLARE`), uma operação de infraestrutura que o C# não faz nativamente."

### 6.3. Sobre Frontend e Fluxo da Aplicação

**Q8: Como a interface lida com a demora do servidor em responder aos dados do gráfico? A tela trava?**
> **Resposta Esperada:** "Não. Implementamos um gerenciamento de estado assíncrono no React. Assim que o componente é montado no `useEffect`, ativamos uma flag `isLoading = true`, que renderiza um componente visual de carregamento (*Loading Spinner*). A interface continua 100% responsiva. Quando a Promise do Axios ou Fetch é resolvida, atualizamos o estado com os dados e a flag muda para falso, renderizando o gráfico suavemente. Se houver erro, a exceção é pega no bloco `.catch()` e exibimos uma mensagem amigável."

**Q9: O projeto de vocês possui autenticação ou controle de acesso (Login/Senha)?**
*(Nota: Adapte esta resposta com base na realidade atual do seu repositório)*
> **Resposta Esperada:** "Neste momento, avaliamos o MVP (Produto Mínimo Viável) e focamos o desenvolvimento no *core business*: a infraestrutura de dados robusta, rastreamento de chamados e extração de KPIs gerenciais no Dashboard. O modelo relacional já prevê a arquitetura de identificadores. A próxima evolução mapeada no escopo é implementar autenticação robusta utilizando Identity Server (com tokens JWT) na API e Context API no React para gerenciar permissões (Role-Based Access Control)."

### 6.4. Expansão: Código, Testes e Cenários de Falha

**Q10: Por que usar Entity Framework e LINQ no código ao invés de criar Procedures diretamente no Banco de Dados (PostgreSQL) para buscar os gráficos?**
> **Resposta Esperada:** "O uso do EF Core com LINQ prioriza a facilidade de manutenção e a portabilidade do código. Manter a lógica de negócios e as regras de sumarização escritas em C# dentro da API significa que nossa regra está centralizada e sob controle de versão contínuo (Git), além de ser facilmente testável via testes unitários. Stored Procedures no banco quebram essa coesão, espalham regras de negócio no SGBD e dificultam uma eventual migração de banco no futuro."

**Q11: Como a aplicação lida com a data/hora da abertura de um chamado, evitando problemas com fusos horários (TimeZones)?**
> **Resposta Esperada:** "Trabalhar com dados temporais é crítico. A melhor prática implementada no .NET é salvar qualquer registro de data/hora no padrão `UTC` (`DateTime.UtcNow`). Dessa forma, a responsabilidade de converter e apresentar a data com o fuso horário local correto fica por conta do Frontend (React), que adapta o horário (como UTC-3 no Brasil) baseado na localização do navegador do usuário. Isso previne que o banco fique acoplado a um fuso específico."

**Q12: Se o banco de dados cair ou a conexão for perdida, como a API e o Frontend se comportam? O usuário vê um erro genérico do sistema ou um travamento letal?**
> **Resposta Esperada:** "Se o banco falhar, o Entity Framework lançará uma exceção, porém a aplicação está preparada via Middlewares nativos do .NET e tratamento no Frontend. O backend retorna um erro HTTP `500 Internal Server Error` (nunca um stack trace legível que comprometa a segurança). O React, no bloco `.catch()` do Axios/Fetch, intercepta esse status e, em vez de 'quebrar', muda seu estado para exibir um componente de erro amigável na UI, orientando o usuário a tentar novamente mais tarde."

**Q13: Vocês realizaram Testes Unitários ou TDD durante a elaboração do projeto?**
*(Nota: Adapte dependendo se há testes na sua base)*
> **Resposta Esperada:** "Nossa arquitetura nasceu **pronta para testes**, devido ao uso intensivo de Injeção de Dependência e DTOs. Isso nos permite isolar o banco de dados original e criar *Mocks* usando ferramentas como `xUnit` e base em memória. O foco inicial foi garantir os testes de integração e o fluxo fim-a-fim (API para React), mas a estrutura modular já facilita imensamente que testes unitários cubram as regras de negócio dos *Controllers* na próxima sprint."

**Q14: Como a aplicação lida com falhas de segurança básicas, como Cross-Site Scripting (XSS) e Cross-Origin (CORS)?**
> **Resposta Esperada:** "Implementamos segurança nas duas pontas. No Backend, o CORS está estritamente configurado para permitir requisições apenas da origem/porta exata do nosso Frontend React, blindando a API contra sites maliciosos de terceiros tentando consumir endpoints. No Frontend, o próprio React aplica um filtro natural (escaping) em todos os valores dinâmicos passados via JSX, neutralizando qualquer script injetado antes que ele seja executado pelo DOM do navegador, prevenindo ataques de XSS."

**Q15: Supondo que em versões futuras o "Chamado" permita anexar arquivos (PDFs, Imagens). Onde vocês salvariam? Dentro do PostgreSQL?**
> **Resposta Esperada:** "Não. Salvar arquivos binários (BLOBs) diretamente na base de dados relacional onera drasticamente o custo e o tempo de backup, além de estourar a memória do banco de dados. A abordagem arquitetural correta seria fazer upload dos arquivos em um *Object Storage* (como Amazon S3, Azure Blob Storage ou até um servidor de arquivos local persistido), e apenas salvar a URL ou a referência (String) desse arquivo na tabela do banco."