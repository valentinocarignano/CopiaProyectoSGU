using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IMateriaRepository : IRepository<Materia>
    {
        new Task<IEnumerable<Materia>> FindAllAsync();
        new Task<IEnumerable<Materia>> FindByConditionAsync(Expression<Func<Materia, bool>> expression);
    }
}