using System.Text.Json.Serialization;

namespace CtedCatalogo.Api.Models;

public class ModuloCurso
{
    public int Id { get; set; }
    public int Ordem { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string DescricaoAulas { get; set; } = string.Empty;

    public int CursoId { get; set; }

    [JsonIgnore]
    public Curso? Curso { get; set; }
}