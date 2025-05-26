using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IExamenRepository : IRepository<Examen>
    {
        new Task<IEnumerable<Examen>> FindAllAsync();
        new Task<IEnumerable<Examen>> FindByConditionAsync(Expression<Func<Examen, bool>> expression);
    }
}