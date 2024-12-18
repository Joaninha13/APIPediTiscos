using System.ComponentModel.DataAnnotations.Schema;

namespace APIPediTiscos.Entities;

public class Categorias{

    public int Id { get; set; }

    public string? Nome { get; set; }

    public int? Ordem { get; set; }

    public string? UrlImagem { get; set; }
    public byte[]? Imagem { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

}
