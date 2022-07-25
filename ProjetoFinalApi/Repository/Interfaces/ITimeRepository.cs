using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Repository.Interfaces;

public interface ITimeRepository : IRepository<Time>
{
    Task<PagedList<Time>> GetTimesAsync(PagedListDefaultParameters pagedListDefaultParameters);

    Task<IEnumerable<Jogador>> GetJogadoresTimeAsync(Expression<Func<Time, bool>> predicate);

    //IEnumerable<Torneio> GetTorneiosTime();

    //IEnumerable<Partida> GetPartidas();

    //IEnumerable<Transferencia> GetTransferencias();
}
