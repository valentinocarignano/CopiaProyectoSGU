using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class DiaHorarioRepository : Repository<DiaHorario>, IDiaHorarioRepository
    {
        public DiaHorarioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
