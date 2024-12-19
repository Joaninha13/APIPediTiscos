using Microsoft.EntityFrameworkCore;

using APIPediTiscos.Data;
using APIPediTiscos.Entities;

namespace APIPediTiscos.Repositories;

public class ItensEncomendadoRepository : IItensEncomendado{

    private readonly ApplicationDbContext dbContext;

    public ItensEncomendadoRepository(ApplicationDbContext dbContext){
        this.dbContext = dbContext;
    }


    // Add to encomenda this produto with this quantidade and get the stock - quantidade in produtos
    public async Task<ItensEncomendados> AddItemToEncomendaAsync(int encomendaId, int produtoId, int quantidade){

        var item = await dbContext.ItensEncomendados.Where(p => p.Id == encomendaId && p.ProdutoId == produtoId).FirstAsync();

        if (item != null){


            var produto2 = await dbContext.Produtos.FindAsync(produtoId);

            // dar trow se o stock for menor que a quantidade
            if (produto2.Stock < quantidade)
                throw new Exception("Stock insuficiente");


            produto2.Stock -= quantidade;
            dbContext.Produtos.Update(produto2);

            item.Quantidade += quantidade;
            dbContext.ItensEncomendados.Update(item);

            await dbContext.SaveChangesAsync();
            return item;
        }

        var itemEncomendado = new ItensEncomendados
        {
            EncomendaId = encomendaId,
            ProdutoId = produtoId,
            Quantidade = quantidade
        };

        await dbContext.ItensEncomendados.AddAsync(itemEncomendado);

        var produto = await dbContext.Produtos.FindAsync(produtoId);
        produto.Stock -= quantidade;

        dbContext.Produtos.Update(produto);

        await dbContext.SaveChangesAsync();

        return itemEncomendado;

    }

    public async Task<ItensEncomendados> DeleteItemFromEncomenda(int id){

        var item = await dbContext.ItensEncomendados.FindAsync(id);

        if (item != null){
            var produto = await dbContext.Produtos.FindAsync(item.ProdutoId);
            produto.Stock += item.Quantidade;

            dbContext.Produtos.Update(produto);
            dbContext.ItensEncomendados.Remove(item);

            await dbContext.SaveChangesAsync();
            return item;
        }

        return null;

    }

    public async Task<IEnumerable<ItensEncomendados>> GetItensEncomendadosByEncomendaAsync(int encomendaId){

        return await dbContext.ItensEncomendados.Where(p => p.EncomendaId == encomendaId).ToListAsync();

    }

    public async Task<ItensEncomendados> RefreshItemAsync(int encomendaId, int produtoId, int quantidade){

        var item = await dbContext.ItensEncomendados.Where(p => p.EncomendaId == encomendaId && p.ProdutoId == produtoId).FirstAsync();

        if (item != null){

            var produto = await dbContext.Produtos.FindAsync(item.ProdutoId);
            produto.Stock += item.Quantidade;

            dbContext.Produtos.Update(produto);

            item.Quantidade = quantidade;
            dbContext.ItensEncomendados.Update(item);

            var produto2 = await dbContext.Produtos.FindAsync(produtoId);
            produto2.Stock -= quantidade;

            dbContext.Produtos.Update(produto2);

            await dbContext.SaveChangesAsync();
            return item;
        }

        return null;

    }
}
