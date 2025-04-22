using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class NotaAlumnoRepository : Repository<NotaAlumno>, INotaAlumnoRepository
    {
        public NotaAlumnoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
