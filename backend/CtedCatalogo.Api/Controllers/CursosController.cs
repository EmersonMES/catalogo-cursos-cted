using CtedCatalogo.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CtedCatalogo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ListarCursos(
        [FromQuery] string? pesquisa,
        [FromQuery] string? categoria,
        [FromQuery] string? modalidade,
        [FromQuery] string? status)
    {
        var query = _context.Cursos
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(pesquisa))
        {
            var termo = pesquisa.Trim().ToLower();
            query = query.Where(c =>
                c.NomeCompleto.ToLower().Contains(termo) ||
                c.NomeBreve.ToLower().Contains(termo));
        }

        if (!string.IsNullOrWhiteSpace(categoria))
        {
            query = query.Where(c => c.Categoria == categoria);
        }

        if (!string.IsNullOrWhiteSpace(modalidade))
        {
            query = query.Where(c => c.Modalidade == modalidade);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(c => c.Status == status);
        }

        var cursos = await query
            .OrderBy(c => c.NomeCompleto)
            .Select(c => new
            {
                c.Id,
                c.NomeCompleto,
                c.NomeBreve,
                c.Descricao,
                c.Categoria,
                c.Modalidade,
                c.CargaHoraria,
                c.PublicoAlvo,
                c.Status,
                c.ImagemApresentacao,
                c.DataDisponibilizacao
            })
            .ToListAsync();

        return Ok(new
        {
            quantidadeResultados = cursos.Count,
            resultados = cursos
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> ObterDetalhesCurso(int id)
    {
        var curso = await _context.Cursos
            .AsNoTracking()
            .Include(c => c.Modulos.OrderBy(m => m.Ordem))
            .FirstOrDefaultAsync(c => c.Id == id);

        if (curso == null)
            return NotFound(new { mensagem = "Curso não encontrado." });

        return Ok(curso);
    }

    [HttpGet("filtros")]
    public async Task<IActionResult> ObterOpcoesFiltros()
    {
        var categorias = await _context.Cursos.Select(c => c.Categoria).Distinct().OrderBy(x => x).ToListAsync();
        var modalidades = await _context.Cursos.Select(c => c.Modalidade).Distinct().OrderBy(x => x).ToListAsync();
        var status = await _context.Cursos.Select(c => c.Status).Distinct().OrderBy(x => x).ToListAsync();

        return Ok(new { categorias, modalidades, status });
    }
}