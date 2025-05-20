using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IInscripcionLogic
    {
        Task AltaInscripcion(string IdAlumno, string IdMateria);
        Task BajaInscripcion(string IdMateria, string IdAlumno);
        Task<List<InscripcionDTO>> ObtenerInscripciones();
    }
}
