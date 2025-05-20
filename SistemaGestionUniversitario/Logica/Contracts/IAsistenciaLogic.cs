using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IAsistenciaLogic
    {
        Task AltaAsistencia(int idinscripcion, int iddiahorariomateria, bool estado, DateTime fecha);
        Task<AsistenciaDTO> ActualizarAsistencia(string dnialumno, string materia, int año, int mes, int dia, bool estado);
        Task EliminarAsistencia(string dnialumno, string nombremateria, int año, int mes, int dia);
        //Task<List<Asistencia>> ObtenerAsistencias();
        //IEnumerable<Asistencia> ObtenerAsistenciasPorFecha(DateTime fecha);
    }
}