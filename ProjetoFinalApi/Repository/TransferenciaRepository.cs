using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class TransferenciaRepository : Repository<Transferencia>, ITransferenciaRepository
    {
        public TransferenciaRepository(ApiDbContext context) 
            : base(context)
        {
        }

        public async Task<PagedList<Transferencia>> GetTransferenciasAsync(TransferenciaParameters transferenciaParameters)
        {
            var dataSource = Get();

            if (transferenciaParameters.Predicate is not null)
                dataSource = dataSource.Where(transferenciaParameters.Predicate);

            return await PagedList<Transferencia>.ToPagedListAsync(dataSource,
                transferenciaParameters.PageNumber, transferenciaParameters.PageSize);
        }
    }
}
