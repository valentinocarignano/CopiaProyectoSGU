using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IInscripcionLogic
    {
        void AltaInscripcion(Inscripcion inscripcionAgregar);
        Task<List<Inscripcion>> ObtenerInscripciones();
        void BajaInscripcion(int idMateria, int idAlumno);
    }
}
