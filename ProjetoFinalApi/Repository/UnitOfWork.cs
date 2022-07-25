using ProjetoFinalApi.Context;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApiDbContext _context;

        private TimeRepository _timeRepository;

        private JogadorRepository _jogadorRepository;

        private TransferenciaRepository _transferenciaRepository;

        private TorneioRepository _torneioRepository;

        private PartidaRepository _partidaRepository;

        private EventoPartidaRepository _eventoPartidaRepository;

        public UnitOfWork(ApiDbContext context)
        {
            _context = context;
        }

        public ITimeRepository TimeRepository
        {
            get 
            { 
                return _timeRepository ??= new TimeRepository(_context); 
            }
        }

        public IJogadorRepository JogadorRepository
        {
            get
            {
                return _jogadorRepository ??= new JogadorRepository(_context);
            }
        }

        public ITransferenciaRepository TransferenciaRepository
        {
            get
            {
                return _transferenciaRepository ??= new TransferenciaRepository(_context);
            }
        }

        public ITorneioRepository TorneioRepository
        {
            get
            {
                return _torneioRepository ??= new TorneioRepository(_context);
            }
        }

        public IPartidaRepository PartidaRepository
        {
            get
            {
                return _partidaRepository ??= new PartidaRepository(_context);
            }
        }

        public IEventoPartidaRepository EventoPartidaRepository
        {
            get
            {
                return _eventoPartidaRepository ??= new EventoPartidaRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
