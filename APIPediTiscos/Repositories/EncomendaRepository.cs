using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Entities;
using APIPediTiscos.Data;

namespace APIPediTiscos.Repositories;

public class EncomendaRepository : IEncomenda{

    private readonly ApplicationDbContext dbContext;
    public EncomendaRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    // Por o Estado da encomenda como 'Concluido'
    public Task<Encomendas> FinhisEncomenda(string clientId)
    {
        var encomenda = dbContext.Encomendas
            .Where(e => e.ClienteId == clientId && e.Estado == "Processamento")
            .FirstOrDefault();

        if (encomenda != null){
            encomenda.Estado = "Concluido";
            dbContext.Encomendas.Update(encomenda);
            dbContext.SaveChangesAsync();
        }

        return Task.FromResult(encomenda);
    }

    // Get all orders by client where state "Confirmado"
    public async Task<IEnumerable<Encomendas>> GetAllEncomendasByClienteAsync(string clientId){

        return await dbContext.Encomendas
            .Where(e => e.ClienteId == clientId && e.Estado == "Confirmado")
            .ToListAsync();

    }

    // Create a new order for a client allways in state "Processamento" and if the client has an order in state "Processamento" return that order
    public async Task<Encomendas> StartNewEncomendaAsync(string clientId){

        var encomenda = await dbContext.Encomendas
            .Where(e => e.ClienteId == clientId && e.Estado == "Processamento")
            .FirstOrDefaultAsync();

        if (encomenda == null){

            encomenda = new Encomendas{
                ClienteId = clientId,
                DataEncomenda = DateTime.Now,
                Estado = "Processamento",
                Total = 0
            };
            dbContext.Encomendas.Add(encomenda);
            await dbContext.SaveChangesAsync();
        }

        return encomenda;

    }
}
