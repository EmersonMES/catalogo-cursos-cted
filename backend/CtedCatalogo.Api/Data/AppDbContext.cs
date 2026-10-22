using CtedCatalogo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CtedCatalogo.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<ModuloCurso> ModulosCursos => Set<ModuloCurso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Curso>()
            .HasMany(c => c.Modulos)
            .WithOne(m => m.Curso)
            .HasForeignKey(m => m.CursoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}