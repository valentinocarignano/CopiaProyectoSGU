using Datos.Contexts;
using Datos.Repositories.Contracts;
using Entidades.Entities;

namespace Datos.Repositories.Implementations
{
    public class ProfesorMateriaRepository : Repository<ProfesorMateria>, IProfesorMateriaRepository
    {
        public ProfesorMateriaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}