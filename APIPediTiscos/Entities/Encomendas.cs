using APIPediTiscos.Data;

namespace APIPediTiscos.Entities;

public class Encomendas{


    public int Id { get; set; }

    public string? ClienteId { get; set; }
    public ApplicationUser Cliente { get; set; }

    public DateTime DataEncomenda { get; set; }

    public string? Estado { get; set; } //Processamento, Concluido, Confirmado, Rejeitado

    public decimal? Total { get; set; }

}
