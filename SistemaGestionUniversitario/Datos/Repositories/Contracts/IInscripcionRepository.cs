using Entidades.Entities;
using System.Linq.Expressions;

namespace Datos.Repositories.Contracts
{
    public interface IInscripcionRepository : IRepository<Inscripcion>
    {
        new Task<IEnumerable<Inscripcion>> FindAllAsync();
        new Task<IEnumerable<Inscripcion>> FindByConditionAsync(Expression<Func<Inscripcion, bool>> expression);
    }
}