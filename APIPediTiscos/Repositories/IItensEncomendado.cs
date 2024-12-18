using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface IItensEncomendado{

    Task<IEnumerable<ItensEncomendados>> GetItensEncomendadosByEncomendaAsync(int encomendaId);
    Task<ItensEncomendados> AddItemToEncomendaAsync(int encomendaId, int produtoId, int quantidade);

    Task<ItensEncomendados> RefreshItemAsync(int encomendaId, int produtoId, int quantidade);

    Task<ItensEncomendados> DeleteItemFromEncomenda(int id);

}
