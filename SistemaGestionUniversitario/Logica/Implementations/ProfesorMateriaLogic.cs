using Datos.Repositories.Contracts;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class ProfesorMateriaLogic : IProfesorMateriaLogic
    {
        private IProfesorMateriaRepository _profesorMateriaRepository;

        public ProfesorMateriaLogic(IProfesorMateriaRepository profesorMateriaRepository)
        {
            _profesorMateriaRepository = profesorMateriaRepository;
        }
    }
}