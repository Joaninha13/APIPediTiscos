using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public interface IEncomenda{

    // Get all orders by client where state "Confirmado"
    Task<IEnumerable<Encomendas>> GetAllEncomendasByClienteAsync(string clientId);

    // Create a new order for a client allways in state "Processamento" and if the client has an order in state "Processamento" return that order
    Task<Encomendas> StartNewEncomendaAsync(string clientId);
}
