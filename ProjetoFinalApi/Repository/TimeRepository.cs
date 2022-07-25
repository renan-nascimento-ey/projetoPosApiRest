using Microsoft.EntityFrameworkCore;
using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class TimeRepository : Repository<Time>, ITimeRepository
    {
        public TimeRepository(ApiDbContext context) 
            : base(context)
        {
        }

        //public IEnumerable<Jogador> GetJogadores()
        //{
        //    return (IEnumerable<Jogador>)Get().Include(t => t.Jogadores).Select(t => t.Jogadores);
        //}

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
