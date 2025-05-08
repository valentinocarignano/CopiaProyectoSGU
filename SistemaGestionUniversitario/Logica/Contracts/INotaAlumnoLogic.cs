using Entidades.DTOs;

namespace Logica.Contracts
{
    public interface INotaAlumnoLogic
    {
        Task AltaNotaAlumno(int nota, int idAlumno, int idExamen);
        Task BajaNotaAlumno(int idAlumno, int idExamen);
        Task<NotaAlumnoDTO> ActualizacionNotaAlumno(int nota, int idAlumno, int idExamen);
        Task<List<NotaAlumnoDTO>> ObtenerNotas();
        Task<List<NotaAlumnoDTO>> ObtenerNotasPorMateria(int idMateria);
        Task<List<NotaAlumnoDTO>> ObtenerNotasPorAlumno(int idAlumno);
    }
}
