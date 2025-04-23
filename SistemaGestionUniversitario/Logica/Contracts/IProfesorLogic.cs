using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IProfesorLogic
    {
        Task AltaProfesor(Usuario usuario, DateTime? fechaInicioContrato);
        Task BajaProfesor(string documento);
        //Task ActualizacionProfesor(string documento, ModificarProfesorDTO profesorActualizar);
        Task<List<Profesor>> ObtenerProfesores();
        Task<Profesor?> ObtenerProfesorID(int id);
    }
}