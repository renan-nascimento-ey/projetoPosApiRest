using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;

namespace ProjetoFinalApi.Repository.Interfaces;

public interface IPartidaRepository :IRepository<Partida>
{
    Task<PagedList<Partida>> GetPartidasAsync(PartidaParameters partidaParameters);
}
