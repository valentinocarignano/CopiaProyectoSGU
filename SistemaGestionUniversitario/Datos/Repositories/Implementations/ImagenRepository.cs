using Datos.Contexts;
using Datos.Repositories.Contracts;
using Entidades.Entities;

namespace Datos.Repositories.Implementations
{
    public class ImagenRepository : Repository<Imagen>, IImagenRepository
    {
        public ImagenRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
