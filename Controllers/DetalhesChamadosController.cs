using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using easydemandasapi.Models;

namespace easydemandasapi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalhesChamadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public DetalhesChamadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalhesChamado>>> GetDetalhesChamados()
    {
        return await _context.DetalhesChamados.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalhesChamado>> GetDetalhesChamado(int id)
    {
        var detalhesChamado = await _context.DetalhesChamados.FindAsync(id);

        if (detalhesChamado == null)
        {
            return NotFound();
        }

        return detalhesChamado;
    }

    [HttpPost]
    public async Task<ActionResult<DetalhesChamado>> PostDetalhesChamado(DetalhesChamado detalhesChamado)
    {
        _context.DetalhesChamados.Add(detalhesChamado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDetalhesChamado), new { id = detalhesChamado.Id }, detalhesChamado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalhesChamado(int id, DetalhesChamado detalhesChamado)
    {
        if (id != detalhesChamado.Id)
        {
            return BadRequest();
        }

        _context.Entry(detalhesChamado).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalhesChamadoExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetalhesChamado(int id)
    {
        var detalhesChamado = await _context.DetalhesChamados.FindAsync(id);
        if (detalhesChamado == null)
        {
            return NotFound();
        }

        _context.DetalhesChamados.Remove(detalhesChamado);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DetalhesChamadoExists(int id)
    {
        return _context.DetalhesChamados.Any(e => e.Id == id);
    }
}
