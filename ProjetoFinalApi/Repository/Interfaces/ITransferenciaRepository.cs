using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Pagination;

namespace ProjetoFinalApi.Repository.Interfaces;

public interface ITransferenciaRepository : IRepository<Transferencia>
{
    Task<PagedList<Transferencia>> GetTransferenciasAsync(TransferenciaParameters transferenciaParameters);
}
