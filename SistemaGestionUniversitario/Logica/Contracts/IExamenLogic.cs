using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IExamenLogic
    {
        void AltaExamen(Examen examenAgregar);
        Task<List<Examen>> ObtenerExamenes();
        void BajaExamen(int idMateria, int idHorario);
        void ActualizacionExamen(int idMateria, int idHorario, Examen examenActualizar);
    }
}
