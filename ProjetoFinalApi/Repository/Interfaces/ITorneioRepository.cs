using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;

namespace ProjetoFinalApi.Repository.Interfaces;

public interface ITorneioRepository : IRepository<Torneio>
{
    Task<PagedList<Torneio>> GetTorneiosAsync(PagedListDefaultParameters pagedListDefaultParameters);
}
