using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IAsistenciaLogic
    {
        void AltaAsistencia(Asistencia asistenciaAgregar);
        void ActualizarAsistencia(Asistencia asistenciaActualizar, int diaHorarioMateria);
        void EliminarAsistencia(int año, int mes, int dia);
        Task<List<Asistencia>> ObtenerAsistencias();
        IEnumerable<Asistencia> ObtenerAsistenciasPorFecha(DateTime fecha);
    }
}