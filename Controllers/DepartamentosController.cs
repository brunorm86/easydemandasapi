using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;
using Microsoft.AspNetCore.Authorization;

namespace easydemandasapi.Controllers;

[Authorize]
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

    [Authorize(Roles = "Gestor")]
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

    [Authorize(Roles = "Gestor")]
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

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Erro de integridade de dados ao atualizar departamento." });
        }

        return NoContent();
    }

    [Authorize(Roles = "Gestor")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartamento(int id)
    {
        if (id == 9999) return BadRequest(new { mensagem = "Não é possível excluir o departamento padrão do sistema." });

        var departamento = await _context.Departamentos.FindAsync(id);

        if (departamento == null)
        {
            return NotFound(new { mensagem = $"Departamento com ID {id} não encontrado." });
        }

        var empregados = await _context.Empregados.Where(e => e.DepartamentoId == id).ToListAsync();
        foreach(var e in empregados) e.DepartamentoId = 9999;

        var detalhes = await _context.DetalhesChamados.Where(dc => dc.DepartamentoId == id).ToListAsync();
        foreach(var dc in detalhes) dc.DepartamentoId = 9999;

        _context.Departamentos.Remove(departamento);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest(new { mensagem = "Não é possível excluir o departamento pois ele está vinculado a outros registros." });
        }

        return NoContent();
    }
}
