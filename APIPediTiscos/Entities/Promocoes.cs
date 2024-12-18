namespace APIPediTiscos.Entities;

public class Promocoes{

    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFim { get; set; }

    public decimal? Desconto { get; set; }

    public bool Ativa { get; set; }

}
