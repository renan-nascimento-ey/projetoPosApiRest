using ProjetoFinalApi.Context;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApiDbContext _context;

        private TimeRepository _timeRepository;

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
