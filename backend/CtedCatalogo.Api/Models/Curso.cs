namespace CtedCatalogo.Api.Models;

public class Curso
{
    public int Id { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string NomeBreve { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public string Modalidade { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public string PublicoAlvo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ImagemApresentacao { get; set; } = string.Empty;
    public DateTime? DataDisponibilizacao { get; set; }

    public ICollection<ModuloCurso> Modulos { get; set; } = new List<ModuloCurso>();
}