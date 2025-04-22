using Entidades.Entities;
using Datos.Repositories.Contracts;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class AlumnoRepository : Repository<Alumno>, IAlumnoRepository
    {
        public AlumnoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
