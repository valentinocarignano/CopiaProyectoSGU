using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class AsistenciaRepository : Repository<Asistencia>, IAsistenciaRepository
    {
        public AsistenciaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
