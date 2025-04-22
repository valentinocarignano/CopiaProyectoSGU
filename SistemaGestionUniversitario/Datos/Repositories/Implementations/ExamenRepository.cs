using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class ExamenRepository : Repository<Examen>, IExamenRepository
    {
        public ExamenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
