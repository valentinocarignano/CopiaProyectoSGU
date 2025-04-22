using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class InscripcionRepository : Repository<Inscripcion>, IInscripcionRepository
    {
        public InscripcionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
