using ProjetoFinalApi.Context;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Repository.Interfaces;

namespace ProjetoFinalApi.Repository
{
    public class TransferenciaRepository : Repository<Transferencia>, ITransferenciaRepository
    {
        public TransferenciaRepository(ApiDbContext context) 
            : base(context)
        {
        }
    }
}
