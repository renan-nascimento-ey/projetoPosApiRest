using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class TorneioRepository : Repository<Torneio>, ITorneioRepository
    {
        public TorneioRepository(ApiDbContext context) 
            : base(context)
        {
        }

        public async Task<PagedList<Torneio>> GetTorneiosAsync(PagedListDefaultParameters pagedListDefaultParameters)
        {
            return await PagedList<Torneio>.ToPagedListAsync(Get().OrderByDescending(on => on.DataInicio),
                pagedListDefaultParameters.PageNumber, pagedListDefaultParameters.PageSize);
        }
    }
}
