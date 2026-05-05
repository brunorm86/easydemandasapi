using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DependentesController : ControllerBase
{
    private readonly AppDbContext _context;

    public DependentesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dependente>>> GetDependentes()
    {
        var dependentes = await _context.Dependentes
            .Include(d => d.Empregado)
            .Include(d => d.Pessoa)
            .ToListAsync();
        return Ok(dependentes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dependente>> GetDependente(int id)
    {
        var dependente = await _context.Dependentes
            .Include(d => d.Empregado)
            .Include(d => d.Pessoa)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (dependente == null)
        {
            return NotFound(new { mensagem = $"Dependente com ID {id} não encontrado." });
        }

        return Ok(dependente);
    }

    [HttpPost]
    public async Task<ActionResult<Dependente>> PostDependente(Dependente dependente)
    {
        // Ao salvar um novo dependente, validamos as entidades.
        var empregado = await _context.Empregados.FindAsync(dependente.EmpregadoId);
        if (empregado == null)
            return BadRequest(new { mensagem = "EmpregadoId inválido." });

        var pessoa = await _context.Pessoas.FindAsync(dependente.PessoaId);
        if (pessoa == null)
            return BadRequest(new { mensagem = "PessoaId inválido." });

        // Uma Pessoa não pode ser empregada E ser dependente
        // No novo modelo, se o PessoaId estiver na tabela Empregados, é um empregado.
        var isEmpregado = await _context.Empregados.AnyAsync(e => e.PessoaId == dependente.PessoaId);
        if (isEmpregado)
        {
            return BadRequest(new { mensagem = "Um empregado não pode ser cadastrado como dependente." });
        }

        // Previne circularidade e objetos não intencionais no JSON
        dependente.Empregado = null;
        dependente.Pessoa = null;

        _context.Dependentes.Add(dependente);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDependente), new { id = dependente.Id }, dependente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDependente(int id, Dependente dependente)
    {
        if (id != dependente.Id)
        {
            return BadRequest(new { mensagem = "ID da URL não confere com o body." });
        }

        var dependenteExistente = await _context.Dependentes.FindAsync(id);
        if (dependenteExistente == null)
        {
            return NotFound(new { mensagem = $"Dependente com ID {id} não encontrado." });
        }

        var isEmpregado = await _context.Empregados.AnyAsync(e => e.PessoaId == dependente.PessoaId);
        if (isEmpregado)
        {
            return BadRequest(new { mensagem = "Um empregado não pode ser cadastrado como dependente." });
        }

        dependenteExistente.Parentesco = dependente.Parentesco;
        dependenteExistente.EmpregadoId = dependente.EmpregadoId;
        dependenteExistente.PessoaId = dependente.PessoaId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDependente(int id)
    {
        var dependente = await _context.Dependentes.FindAsync(id);
        if (dependente == null)
        {
            return NotFound(new { mensagem = $"Dependente com ID {id} não encontrado." });
        }

        _context.Dependentes.Remove(dependente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
