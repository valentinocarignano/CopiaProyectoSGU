using Entidades.DTOs;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IExamenLogic
    {
        Task AltaExamen(DateTime fecha, string nombreMateria, string descripcionDiaHorario, string tipoExamen);
        Task<ExamenDTO> ActualizacionExamen(int idMateria, int idHorario, int idNuevoDiaHorario);
        Task BajaExamen(int idMateria, int idDiaHorario);
        Task<List<ExamenDTO>> ObtenerExamenes();
        Task<List<ExamenDTO>> ObtenerExamenesPorMateria(string nombreMateria);
    }
}
