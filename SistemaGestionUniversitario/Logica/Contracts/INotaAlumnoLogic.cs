using Entidades.Entities;

namespace Logica.Contracts
{
    public interface INotaAlumnoLogic
    {
        void AltaNotaAlumno(int nota, int idAlumno, int idExamen);
        Task<List<NotaAlumno>> ObtenerNotas();
        void BajaNotaAlumno(int idAlumno, int idExamen);
        void ActualizacionNotaAlumno(int nota, int idAlumno, int idExamen);
    }
}
