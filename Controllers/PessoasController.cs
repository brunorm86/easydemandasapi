// Controllers/PessoasController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

// [ApiController] adiciona comportamentos automáticos úteis:
//   - Valida automaticamente o ModelState (dados recebidos)
//   - Retorna 400 automaticamente se dados obrigatórios faltarem
//   - Lê o body da requisição como JSON automaticamente
[ApiController]

// [Route("api/[controller]")] define o prefixo da URL para todos os endpoints.
// [controller] é substituído pelo nome da classe sem "Controller":
// PessoasController → "Pessoas"
// Resultado: todos os endpoints começam com /api/pessoas
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    // _context é nossa instância do AppDbContext.
    // É através dele que fazemos todas as operações no banco.
    // O "readonly" garante que não podemos reatribuir _context depois do construtor.
    private readonly AppDbContext _context;

    // Construtor com Injeção de Dependência.
    // O .NET vê que o construtor precisa de um AppDbContext
    // e automaticamente cria e injeta um (porque registramos no Program.cs).
    // Você nunca vai chamar "new PessoasController()" manualmente.
    public PessoasController(AppDbContext context)
    {
        _context = context;
    }

    // =====================================================================
    // GET /api/pessoas
    // Retorna todas as pessoas cadastradas.
    //
    // HTTP GET é usado para LEITURA — não modifica dados no servidor.
    // IEnumerable<Pessoa> indica que retornamos uma coleção de pessoas.
    // async/await permite que o servidor atenda outras requisições
    // enquanto espera o banco de dados responder.
    // =====================================================================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
    {
        // _context.Pessoas é o DbSet<Pessoa> — representa a tabela "Pessoas".
        // .ToListAsync() executa: SELECT * FROM "Pessoas"
        // e retorna o resultado como uma List<Pessoa>.
        var pessoas = await _context.Pessoas.ToListAsync();

        // Ok(pessoas) retorna HTTP 200 com as pessoas serializadas em JSON.
        return Ok(pessoas);
    }

    // =====================================================================
    // GET /api/pessoas/5
    // Retorna uma única pessoa pelo ID.
    //
    // {id} na rota é um parâmetro dinâmico — o valor da URL é capturado
    // e passado como parâmetro para o método.
    // Exemplo: GET /api/pessoas/3 → id = 3
    // =====================================================================
    [HttpGet("{id}")]
    public async Task<ActionResult<Pessoa>> GetPessoa(int id)
    {
        // FindAsync busca pelo valor da chave primária.
        // Equivalente a: SELECT * FROM "Pessoas" WHERE "Id" = @id LIMIT 1
        // Retorna null se não encontrar.
        var pessoa = await _context.Pessoas.FindAsync(id);

        // Se a pessoa não foi encontrada, retornamos HTTP 404 Not Found
        // com uma mensagem explicativa em JSON.
        if (pessoa == null)
        {
            return NotFound(new { mensagem = $"Pessoa com ID {id} não encontrada." });
        }

        // HTTP 200 com o produto encontrado
        return Ok(pessoa);
    }

    // =====================================================================
    // POST /api/pessoas
    // Cria uma nova pessoa.
    //
    // [FromBody] indica que o objeto Pessoa vem no corpo (body) da requisição
    // em formato JSON. O .NET desserializa automaticamente.
    //
    // Exemplo de body JSON:
    // {
    //   "nome": "Notebook Dell XPS",
    //   "descricao": "16GB RAM, SSD 512GB",
    //   "preco": 4500.00,
    //   "quantidade": 10
    // }
    // =====================================================================
    [HttpPost]
    public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
    {

        // Adiciona a pessoa à "fila de inserção" do EF.
        // A pessoa ainda NÃO foi salva no banco aqui.
        _context.Pessoas.Add(pessoa);

        // SaveChangesAsync() executa o INSERT no banco de dados:
        // INSERT INTO "Pessoas" ("Nome", "Sobrenome", "Email", "Telefone", "DataNascimento", "NumDependentes")
        // VALUES (@nome, @sobrenome, @email, @telefone, @datanascimento, @numdependentes)
        // Após o SaveChanges, o objeto pessoa.Id é preenchido com o ID gerado pelo banco.
        await _context.SaveChangesAsync();

        // CreatedAtAction retorna HTTP 201 Created.
        // - nameof(GetPessoa): referência ao método que busca por ID
        // - new { id = pessoa.Id }: parâmetro para montar a URL de localização
        // - pessoa: o objeto criado (com o Id preenchido pelo banco)
        //
        // O HTTP 201 inclui um header "Location" com a URL do recurso criado:
        // Location: https://localhost:5217/api/pessoas/1
        return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.Id }, pessoa);
    }

    // =====================================================================
    // PUT /api/pessoas/5
    // Atualiza uma pessoa existente.
    //
    // PUT substitui o recurso completo — você deve enviar todos os campos.
    // Diferente do PATCH, que atualiza apenas campos específicos.
    //
    // O ID vem tanto na URL (/api/pessoas/5) quanto no body do JSON ({"id": 5}).
    // Verificamos se são iguais para evitar atualizações acidentais.
    // =====================================================================
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
    {
        // Verifica se o ID da URL corresponde ao ID da pessoa no body.
        // Evita bugs onde o cliente envia o ID errado no body.
        if (id != pessoa.Id)
        {
            return BadRequest(new { mensagem = "O ID da URL não corresponde ao ID da pessoa no body." });
        }

        // Verificamos se a pessoa existe antes de tentar atualizar.
        var pessoaExistente = await _context.Pessoas.FindAsync(id);

        if (pessoaExistente == null)
        {
            return NotFound(new { mensagem = $"Pessoa com ID {id} não encontrada." });
        }

        // Atualizamos os campos.
        // Não alteramos o Id (chave primária).
        pessoaExistente.Nome = pessoa.Nome;
        pessoaExistente.Sobrenome = pessoa.Sobrenome;
        pessoaExistente.Email = pessoa.Email;
        pessoaExistente.Telefone = pessoa.Telefone;
        pessoaExistente.Endereco = pessoa.Endereco;
        pessoaExistente.Cpf = pessoa.Cpf;
        pessoaExistente.DataNascimento = pessoa.DataNascimento;
        pessoaExistente.NumDependentes = pessoa.NumDependentes;

        // SaveChangesAsync executa o UPDATE no banco:
        // UPDATE "Pessoas"
        // SET "Nome" = @nome, "Sobrenome" = @sobrenome, "Email" = @email, "Telefone" = @telefone, "DataNascimento" = @datanascimento, "NumDependentes" = @numdependentes
        // WHERE "Id" = @id
        await _context.SaveChangesAsync();

        // NoContent() retorna HTTP 204 — operação bem-sucedida, sem body de resposta.
        // É o padrão REST para respostas de PUT/DELETE bem-sucedidos.
        return NoContent();
    }

    // =====================================================================
    // DELETE /api/pessoas/5
    // Remove uma pessoa pelo ID.
    // =====================================================================
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePessoa(int id)
    {
        // Busca a pessoa antes de tentar deletar.
        var pessoa = await _context.Pessoas.FindAsync(id);

        // Se não existe, retorna 404.
        if (pessoa == null)
        {
            return NotFound(new { mensagem = $"Pessoa com ID {id} não encontrada." });
        }

        // Marca a pessoa para remoção na "fila de operações" do EF.
        _context.Pessoas.Remove(pessoa);

        // Executa o DELETE no banco:
        // DELETE FROM "Pessoas" WHERE "Id" = @id
        await _context.SaveChangesAsync();

        // HTTP 204 — pessoa deletada com sucesso, sem body de resposta.
        return NoContent();
    }
}