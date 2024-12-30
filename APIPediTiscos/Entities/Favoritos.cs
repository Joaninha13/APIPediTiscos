using APIPediTiscos.Data;

namespace APIPediTiscos.Entities;

public class Favoritos{

    public int Id { get; set; }

    public string? ClienteId { get; set; }
    public ApplicationUser Cliente { get; set; }

    public int? ProdutoId { get; set; }
    public Produtos Produto { get; set; }

}
