using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class DiaHorarioMateriaRepository : Repository<DiaHorarioMateria>, IDiaHorarioMateriaRepository
    {
        public DiaHorarioMateriaRepository(ApplicationDbContext context) : base(context)
        {
        }       
    }
}
