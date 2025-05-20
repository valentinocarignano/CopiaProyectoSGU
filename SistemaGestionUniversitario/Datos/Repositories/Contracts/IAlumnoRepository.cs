using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IAlumnoRepository : IRepository<Alumno>
    {
        new Task<IEnumerable<Alumno>> FindAllAsync();
        new Task<IEnumerable<Alumno>> FindByConditionAsync(Expression<Func<Alumno, bool>> expression);
    }
}