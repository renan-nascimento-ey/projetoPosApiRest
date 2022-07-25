using ProjetoFinalApi.Models.Data;
using System.Linq.Expressions;

namespace ProjetoFinalApi.Pagination
{
    public class PartidaParameters : PagedListDefaultParameters
    {
        public Expression<Func<Partida, bool>> Predicate { get; set; }
    }
}
