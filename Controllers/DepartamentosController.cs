using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DepartamentosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
    {
        var departamentos = await _context.Departamentos
            .Include(d => d.Responsavel)
            .ToListAsync();

        return Ok(departamentos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Departamento>> GetDepartamento(int id)
    {
        var departamento = await _context.Departamentos
            .Include(d => d.Responsavel)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (departamento == null)
        {
            return NotFound(new { mensagem = $"Departamento com ID {id} não encontrado." });
        }

        return Ok(departamento);
    }

    [HttpPost]
    public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
    {
        // Ao receber um departamento novo, não precisamos que o body contenha o objeto Responsavel completo,
        // mas se enviar, o EF pode tentar criá-lo. Recomendamos limpar a navegação para que o EF 
        // associe apenas o ResponsavelId enviado.
        departamento.Responsavel = null; 

        _context.Departamentos.Add(departamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartamento), new { id = departamento.Id }, departamento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
    {
        if (id != departamento.Id)
        {
            return BadRequest(new { mensagem = "O ID da URL não corresponde ao ID do departamento no body." });
        }

        var departamentoExistente = await _context.Departamentos.FindAsync(id);

        if (departamentoExistente == null)
        {
            return NotFound(new { mensagem = $"Departamento com ID {id} não encontrado." });
        }

        departamentoExistente.Nome = departamento.Nome;
        departamentoExistente.Sigla = departamento.Sigla;
        departamentoExistente.ResponsavelId = departamento.ResponsavelId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartamento(int id)
    {
        var departamento = await _context.Departamentos.FindAsync(id);

        if (departamento == null)
        {
            return NotFound(new { mensagem = $"Departamento com ID {id} não encontrado." });
        }

        _context.Departamentos.Remove(departamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
