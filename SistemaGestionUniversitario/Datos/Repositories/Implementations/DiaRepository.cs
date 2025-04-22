using Entidades.Entities;
using Microsoft.EntityFrameworkCore;
using Datos.Repositories.Contracts;
using Datos.Contexts;

namespace Datos.Repositories.Implementations
{
    public class DiaRepository : Repository<Dia>, IDiaRepository
    {
        public DiaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
