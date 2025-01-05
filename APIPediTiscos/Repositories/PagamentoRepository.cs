using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Data;
using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public class PagamentoRepository : IPagamento
{

    private readonly ApplicationDbContext dbContext;

    public PagamentoRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }

    //Create and put the state as "Pendente"
    public async Task<Pagamentos> AddPagamentoToEncomendaAsync(int encomendaId){

        Pagamentos pagamento = new Pagamentos{
            EncomendaId = encomendaId,
            Valor = await dbContext.Encomendas
            .Where(e => e.Id == encomendaId)
            .Select(e => e.Total)
            .FirstOrDefaultAsync(),
            Estado = "Pendente"
        };

        dbContext.Pagamentos.Add(pagamento);
        await dbContext.SaveChangesAsync();

        return pagamento;

    }

    public async Task<Pagamentos> ReattempPagamentoAsync(int encomendaId){

        var pagamento = await dbContext.Pagamentos
            .Include("Encomenda")
            .FirstOrDefaultAsync(p => p.EncomendaId == encomendaId);

        pagamento.Estado = "Pendente";
        await dbContext.SaveChangesAsync();
        return pagamento;

    }

    public async Task<Pagamentos> GetPagamentoByEncomendaAsync(int encomendaId){

        return await dbContext.Pagamentos
            .Include("Encomenda")
            .FirstOrDefaultAsync(p => p.EncomendaId == encomendaId);

    }
}
