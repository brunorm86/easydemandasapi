// Controllers/DashboardController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easydemandasapi.Data;
using Microsoft.AspNetCore.Authorization;

namespace easydemandasapi.Controllers;

// --- DTOs (Data Transfer Objects) para o Dashboard ---

public class LabelValueDto
{
    public string Label { get; set; } = "";
    public decimal Value { get; set; }
}

public class DashboardChamadosDto
{
    public int TotalChamados { get; set; }
    public int TotalAbertos { get; set; }
    public decimal TotalCusto { get; set; }
    public decimal CustoMedio { get; set; }
    public List<LabelValueDto> ChamadosPorStatus { get; set; } = new();
    public List<LabelValueDto> ChamadosPorCriticidade { get; set; } = new();
    public List<LabelValueDto> TopSolicitantes { get; set; } = new();
    public List<LabelValueDto> DeptQueAbrem { get; set; } = new();
    public List<LabelValueDto> DeptQueRecebem { get; set; } = new();
    public List<LabelValueDto> TopCentrosCusto { get; set; } = new();
    public List<LabelValueDto> TopGargalos { get; set; } = new();
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("chamados")]
    public async Task<ActionResult<DashboardChamadosDto>> GetDashboardChamados()
    {
        // Carrega todos os chamados com suas relações necessárias para as agregações
        var chamados = await _context.Chamados
            .Include(c => c.Detalhes)
                .ThenInclude(d => d!.Departamento)
            .Include(c => c.Solicitante)
                .ThenInclude(e => e!.Departamento)
            // Exclui o empregado "indeterminado" (seed de dados)
            .Where(c => c.SolicitanteId != 9999)
            .ToListAsync();

        var totalChamados = chamados.Count;

        var custoTotal = chamados
            .Where(c => c.Detalhes?.Custo != null)
            .Sum(c => c.Detalhes!.Custo!.Value);

        var chamadosComCusto = chamados.Count(c => c.Detalhes?.Custo != null);
        var custoMedio = chamadosComCusto > 0 ? custoTotal / chamadosComCusto : 0;

        // Chamados agrupados por Status
        var porStatus = chamados
            .GroupBy(c => c.Status)
            .Select(g => new LabelValueDto { Label = g.Key, Value = g.Count() })
            .OrderByDescending(x => x.Value)
            .ToList();

        // Chamados agrupados por Criticidade
        var porCriticidade = chamados
            .Where(c => c.Detalhes != null)
            .GroupBy(c => c.Detalhes!.NivelCriticidade)
            .Select(g => new LabelValueDto { Label = g.Key, Value = g.Count() })
            .OrderByDescending(x => x.Value)
            .ToList();

        // Top 5 empregados que mais abrem chamados
        var topSolicitantes = chamados
            .GroupBy(c => c.SolicitanteId)
            .Select(g =>
            {
                var emp = g.First().Solicitante;
                var nome = emp != null
                    ? $"{emp.Nome} {emp.Sobrenome}".Trim()
                    : $"Emp. #{g.Key}";
                return new LabelValueDto { Label = nome, Value = g.Count() };
            })
            .OrderByDescending(x => x.Value)
            .Take(5)
            .ToList();

        // Top 5 departamentos cujos empregados mais abrem chamados
        var deptQueAbrem = chamados
            .Where(c => c.Solicitante?.DepartamentoId != null)
            .GroupBy(c => c.Solicitante!.DepartamentoId)
            .Select(g =>
            {
                var nome = g.First().Solicitante?.Departamento?.Nome ?? "S/ Departamento";
                return new LabelValueDto { Label = nome, Value = g.Count() };
            })
            .OrderByDescending(x => x.Value)
            .Take(5)
            .ToList();

        // Top 5 departamentos que mais recebem chamados (centro de custo em DetalhesChamado)
        var deptQueRecebem = chamados
            .Where(c => c.Detalhes != null)
            .GroupBy(c => c.Detalhes!.DepartamentoId)
            .Select(g =>
            {
                var nome = g.First().Detalhes?.Departamento?.Nome ?? "S/ Departamento";
                return new LabelValueDto { Label = nome, Value = g.Count() };
            })
            .OrderByDescending(x => x.Value)
            .Take(5)
            .ToList();

        // Top 5 maiores centros de custo (soma de Custo por Departamento)
        var topCentrosCusto = chamados
            .Where(c => c.Detalhes?.Custo != null)
            .GroupBy(c => c.Detalhes!.DepartamentoId)
            .Select(g =>
            {
                var nome = g.First().Detalhes?.Departamento?.Nome ?? "S/ Departamento";
                var total = g.Sum(c => c.Detalhes!.Custo!.Value);
                return new LabelValueDto { Label = nome, Value = total };
            })
            .OrderByDescending(x => x.Value)
            .Take(5)
            .ToList();

        // Top 5 Departamentos com mais chamados Pendentes (Aberto ou Em Andamento) - "Gargalos"
        var topGargalos = chamados
            .Where(c => c.Detalhes != null && (c.Status == "Aberto" || c.Status == "Em Andamento"))
            .GroupBy(c => c.Detalhes!.DepartamentoId)
            .Select(g =>
            {
                var nome = g.First().Detalhes?.Departamento?.Nome ?? "S/ Departamento";
                return new LabelValueDto { Label = nome, Value = g.Count() };
            })
            .OrderByDescending(x => x.Value)
            .Take(5)
            .ToList();

        var totalAbertos = chamados.Count(c => c.Status == "Aberto");

        return Ok(new DashboardChamadosDto
        {
            TotalChamados = totalChamados,
            TotalAbertos = totalAbertos,
            TotalCusto = custoTotal,
            CustoMedio = custoMedio,
            ChamadosPorStatus = porStatus,
            ChamadosPorCriticidade = porCriticidade,
            TopSolicitantes = topSolicitantes,
            DeptQueAbrem = deptQueAbrem,
            DeptQueRecebem = deptQueRecebem,
            TopCentrosCusto = topCentrosCusto,
            TopGargalos = topGargalos
        });
    }
}
