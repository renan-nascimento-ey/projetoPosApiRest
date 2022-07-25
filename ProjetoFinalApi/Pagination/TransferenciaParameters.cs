using ProjetoFinalApi.Models.Data;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Pagination;

public class TransferenciaParameters : PagedListDefaultParameters
{
    public Expression<Func<Transferencia, bool>> Predicate { get; set; }
}
