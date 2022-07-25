using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class PartidaRepository : Repository<Partida>, IPartidaRepository
    {
        public PartidaRepository(ApiDbContext context) 
            : base(context)
        {
        }

        public async Task<PagedList<Partida>> GetPartidasAsync(PartidaParameters partidaParameters)
        {
            var dataSource = Get();

            if (partidaParameters.Predicate is not null)
                dataSource = dataSource.Where(partidaParameters.Predicate);

            return await PagedList<Partida>.ToPagedListAsync(dataSource,
                partidaParameters.PageNumber, partidaParameters.PageSize);
        }
    }
}
