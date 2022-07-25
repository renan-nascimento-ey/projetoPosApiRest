using ProjetoFinalApi.Models.Data;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Pagination;

public class TimeParameters : PagedListDefaultParameters
{
    public Expression<Func<Time, bool>> Predicate { get; set; }
}
