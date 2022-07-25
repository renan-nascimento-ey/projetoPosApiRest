using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;

namespace ProjetoFinalApi.Repository.Interfaces;

public interface IJogadorRepository : IRepository<Jogador>
{
    Task<PagedList<Jogador>> GetJogadoresAsync(PagedListDefaultParameters pagedListDefaultParameters);
}
