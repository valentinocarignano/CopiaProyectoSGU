using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IInscripcionLogic
    {
        Task AltaInscripcion(string dniAlumno, string nombreMateria);
        Task BajaInscripcion(string dniAlumno, string nombreMateria);
        Task<List<InscripcionDTO>> ObtenerInscripciones();
    }
}
