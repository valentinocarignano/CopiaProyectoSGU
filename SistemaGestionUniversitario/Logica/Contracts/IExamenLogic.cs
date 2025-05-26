using Entidades.DTOs.Respuestas;

namespace Logica.Contracts
{
    public interface IExamenLogic
    {
        Task AltaExamen(string nombreMateria, string descripcionDiaHorario, string tipoExamen);
        Task<ExamenDTO> ActualizacionExamen(string nombreMateria, string descripcionDiaHorario, int idNuevoDiaHorario);
        Task BajaExamen(string nombreMateria, string descripcionDiaHorario);
        Task<List<ExamenDTO>> ObtenerExamenes();
        Task<List<ExamenDTO>> ObtenerExamenesPorMateria(string nombreMateria);
    }
}