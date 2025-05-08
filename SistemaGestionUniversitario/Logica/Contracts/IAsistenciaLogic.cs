using Entidades.DTOs;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IAsistenciaLogic
    {
        Task AltaAsistencia(int idinscripcion, int iddiahorariomateria, bool estado, DateTime fecha);
        Task<AsistenciaDTO> ActualizarAsistencia(int idasistencia, bool estado, string dia, string horario, string materia, string nombrealumno, DateTime fecha);
        void EliminarAsistencia(int año, int mes, int dia);
        Task<List<Asistencia>> ObtenerAsistencias();
        IEnumerable<Asistencia> ObtenerAsistenciasPorFecha(DateTime fecha);
    }
}