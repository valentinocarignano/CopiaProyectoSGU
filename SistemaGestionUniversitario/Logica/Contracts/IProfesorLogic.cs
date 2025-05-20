using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IProfesorLogic
    {
        Task AltaProfesor(Usuario usuario, DateTime? fechaInicioContrato);
        Task BajaProfesor(string documento);
        Task ActualizacionProfesor(Usuario usuario);
        Task<List<ProfesorDTO>> ObtenerProfesores();
        Task<ProfesorDTO> ObtenerProfesorDNI(string dni);
    }
}