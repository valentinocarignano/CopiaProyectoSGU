using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IAlumnoLogic
    {
        Task AltaAlumno(Usuario usuario, DateTime? fechaIngreso);
        Task BajaAlumno(string documento);
        Task ActualizacionAlumno(Usuario usuario);
        Task<List<AlumnoDTO>> ObtenerAlumnos();
        Task<AlumnoDTO> ObtenerAlumnoDNI(string dni);
    }
}