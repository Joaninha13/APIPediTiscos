namespace APIPediTiscos.Entities;

public class Pagamentos{

    public int Id { get; set; }

    public int? EncomendaId { get; set; }
    public Encomendas Encomenda { get; set; }

    public DateTime DataPagamento { get; set; }

    public decimal? Valor { get; set; }

    public string? Estado { get; set; } //Pendente, Pago, Rejeitado 

}
