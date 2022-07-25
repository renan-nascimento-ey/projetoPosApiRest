using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Repository.Interfaces
{
    public interface ITimeRepository : IRepository<Time>
    {
        PagedList<Time> GetTimes(TimeParameters timeParameters);

        IEnumerable<Jogador> GetJogadoresTime(Expression<Func<Time, bool>> predicate);

        //IEnumerable<Torneio> GetTorneiosTime();

        //IEnumerable<Partida> GetPartidas();

        //IEnumerable<Transferencia> GetTransferencias();
    }
}
