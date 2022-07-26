using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> TimeTorneioAsync(int id, int timeId)
        {
            var torneio = await Get().Include(x => x.TorneioTimes).FirstOrDefaultAsync(x => x.Id == id);
            return torneio.TorneioTimes.Any(x => x.TimeId == timeId);
        }
    }
}
