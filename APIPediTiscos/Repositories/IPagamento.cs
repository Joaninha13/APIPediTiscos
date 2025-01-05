using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface IPagamento{

    Task<Pagamentos> GetPagamentoByEncomendaAsync(int encomendaId);
    //Create and put the state as "Pendente"
    Task<Pagamentos> AddPagamentoToEncomendaAsync(int encomendaId);
    Task<Pagamentos> ReattempPagamentoAsync(int encomendaId);


}
