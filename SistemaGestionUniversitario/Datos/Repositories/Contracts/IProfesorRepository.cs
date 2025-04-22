using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IProfesorRepository : IRepository<Profesor>
    {
        new Task<IEnumerable<Profesor>> FindAllAsync();
        new Task<IEnumerable<Profesor>> FindByConditionAsync(Expression<Func<Profesor, bool>> expression);
    }
}