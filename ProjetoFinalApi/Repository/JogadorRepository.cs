using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class JogadorRepository : Repository<Jogador>, IJogadorRepository
    {
        public JogadorRepository(ApiDbContext context) 
            : base(context)
        {
        }

        public async Task<PagedList<Jogador>> GetJogadoresAsync(PagedListDefaultParameters pagedListDefaultParameters)
        {
            return await PagedList<Jogador>.ToPagedListAsync(Get().OrderBy(on => on.Nome),
                pagedListDefaultParameters.PageNumber, pagedListDefaultParameters.PageSize);
        }
    }
}
