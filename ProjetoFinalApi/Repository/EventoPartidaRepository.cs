using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class EventoPartidaRepository : Repository<EventoPartida>, IEventoPartidaRepository
    {
        public EventoPartidaRepository(ApiDbContext context) 
            : base(context)
        {
        }
    }
}
