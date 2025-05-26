using Datos.Repositories.Contracts;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class DiaHorarioMateriaLogic : IDiaHorarioMateriaLogic
    {
        IDiaHorarioMateriaRepository _diaHorarioMateriaRepository;

        public DiaHorarioMateriaLogic(IDiaHorarioMateriaRepository diaHorarioMateriaRepository)
        {
            _diaHorarioMateriaRepository = diaHorarioMateriaRepository;
        }
    }
}