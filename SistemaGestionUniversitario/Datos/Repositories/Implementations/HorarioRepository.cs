using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class HorarioRepository:Repository<Horario>,IHorarioRepository
    {
        public HorarioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
