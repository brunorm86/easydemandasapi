using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpregadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpregadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregados()
    {
        var empregados = await _context.Empregados.ToListAsync();
        return Ok(empregados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empregado>> GetEmpregado(int id)
    {
        var empregado = await _context.Empregados.FindAsync(id);

        if (empregado == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        return Ok(empregado);
    }

    [HttpPost]
    public async Task<ActionResult<Empregado>> PostEmpregado(Empregado empregado)
    {
        _context.Empregados.Add(empregado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpregado), new { id = empregado.Id }, empregado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmpregado(int id, Empregado empregado)
    {
        if (id != empregado.Id)
        {
            return BadRequest(new { mensagem = "O ID da URL não corresponde ao ID do empregado no body." });
        }

        var empregadoExistente = await _context.Empregados.FindAsync(id);

        if (empregadoExistente == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        empregadoExistente.Nome = empregado.Nome;
        empregadoExistente.Sobrenome = empregado.Sobrenome;
        empregadoExistente.Email = empregado.Email;
        empregadoExistente.Telefone = empregado.Telefone;
        empregadoExistente.Endereco = empregado.Endereco;
        empregadoExistente.Cpf = empregado.Cpf;
        empregadoExistente.DataNascimento = empregado.DataNascimento;
        empregadoExistente.Cargo = empregado.Cargo;
        empregadoExistente.DataContratacao = empregado.DataContratacao;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpregado(int id)
    {
        var empregado = await _context.Empregados.FindAsync(id);

        if (empregado == null)
        {
            return NotFound(new { mensagem = $"Empregado com ID {id} não encontrado." });
        }

        _context.Empregados.Remove(empregado);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
