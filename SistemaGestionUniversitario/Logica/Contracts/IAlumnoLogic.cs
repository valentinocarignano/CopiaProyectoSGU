using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IAlumnoLogic
    {
        Task AltaAlumno(Usuario usaurio, DateTime? fechaIngreso);
        Task BajaAlumno(string documento);
        //Task ActualizacionAlumno(string documento, ModificarAlumnoDTO alumnoActualizar);
        Task<Alumno?> ObtenerAlumnoID(int id);
    }
}
