using Logica.Contracts;

namespace Logica.Implementations
{
    public class DiaHorarioMateriaLogic : IDiaHorarioMateriaLogic
    {
        IDiaHorarioMateriaLogic _diaHorarioMateriaRepository;

        public DiaHorarioMateriaLogic(IDiaHorarioMateriaLogic diaHorarioMateriaRepository)
        {
            _diaHorarioMateriaRepository = diaHorarioMateriaRepository;
        }
    }
}