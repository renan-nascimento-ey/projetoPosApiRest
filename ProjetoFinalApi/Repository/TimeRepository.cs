using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Repository
{
    public class TimeRepository : Repository<Time>, ITimeRepository
    {
        public TimeRepository(ApiDbContext context) 
            : base(context)
        {
        }

        public PagedList<Time> GetTimes(TimeParameters timeParameters)
        {
            return PagedList<Time>.ToPagedList(Get().OrderBy(on => on.Nome), 
                timeParameters.PageNumber, timeParameters.PageSize);
        }

        public IEnumerable<Jogador> GetJogadoresTime(Expression<Func<Time, bool>> predicate)
        {
            var jogadores = new List<Jogador>();

            var timeJogadores = Get().Include(t => t.Jogadores).Where(predicate).FirstOrDefault();

            return timeJogadores is not null ? timeJogadores.Jogadores.OrderBy(j => j.Nome).AsEnumerable() : null;
        }
       
        //public IEnumerable<Partida> GetPartidas()
        //{
        //    var partidas = new List<Partida>();

        //    partidas.AddRange((IEnumerable<Partida>)Get().Include(t => t.PartidasCasa).Select(t => t.PartidasCasa));
        //    partidas.AddRange((IEnumerable<Partida>)Get().Include(t => t.PartidasVisitante).Select(t => t.PartidasVisitante));

        //    return partidas;
        //}

        //public IEnumerable<Torneio> GetTorneios()
        //{
        //    var torneios = new List<Torneio>();

        //    var timeTorneios = (IEnumerable<Partida>)Get().Include(t => t.TimeTorneios).ThenInclude(t => t.Torneio).Select(t => t.TimeTorneios);

        //    foreach (var item in timeTorneios)
        //    {
        //        torneios.Add(item.Torneio);
        //    }

        //    return torneios;
        //}

        //public IEnumerable<Transferencia> GetTransferencias()
        //{
        //    var transferencias = new List<Transferencia>();

        //    transferencias.AddRange((IEnumerable<Transferencia>)Get().Include(t => t.TransferenciasOrigem).Select(t => t.TransferenciasOrigem));
        //    transferencias.AddRange((IEnumerable<Transferencia>)Get().Include(t => t.TransferenciasDestino).Select(t => t.TransferenciasDestino));

        //    return transferencias;
        //}
    }
}
