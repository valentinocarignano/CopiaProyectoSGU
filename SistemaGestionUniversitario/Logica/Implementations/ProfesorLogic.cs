using Datos.Repositories.Contracts;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class ProfesorLogic : IProfesorLogic
    {
        private IProfesorRepository _profesorRepository;

        public ProfesorLogic(IProfesorRepository profesorRepository)
        {
            _profesorRepository = profesorRepository;
        }

        public async Task AltaProfesor(Usuario usuario, DateTime? fechaInicioContrato)
        {
            

            if (fechaInicioContrato == null)
            {
                throw new ArgumentNullException("Se debe asignar una fecha de inicio de contrato.");
            }

            if (usuario == null)
            {
                throw new ArgumentNullException("El profesor debe estar vinculado a un usuario.");
            }
            
            Profesor profesorNuevo = new Profesor()
            {
                FechaInicioContrato = fechaInicioContrato.Value,
                Usuario = usuario,
            };

            await _profesorRepository.AddAsync(profesorNuevo);
            await _profesorRepository.SaveAsync();
        }
        public async Task BajaProfesor(string documento)
        {
            if (string.IsNullOrEmpty(documento) || !ValidacionesCampos.DocumentoEsValido(documento))
            {
                throw new ArgumentException("El documento ingresado no es valido.");
            }

            Profesor? profesorExistente = (await _profesorRepository.FindByConditionAsync(t => t.Usuario.DNI == documento)).FirstOrDefault();

            if (profesorExistente == null)
            {
                throw new ArgumentException("No hay ningun profesor vinculado con el documento ingresado..");
            }

            _profesorRepository.Remove(profesorExistente);
            await _profesorRepository.SaveAsync();
        }

        public async Task ActualizacionProfesor(string documento, ModificarProfesorDTO profesorActualizar)
        {
            if (profesorActualizar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun profesor.");
            }

            if (profesorActualizar.Usuario == null)
            {
                throw new ArgumentNullException("El profesor debe estar vinculado a un usuario.");
            }

            await ProfesorRepos.Update(profesorActualizar.Usuario.DNI, profesorActualizar);

        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            return await ProfesorRepos.Get();
        }

        public async Task<Profesor?> ObtenerProfesorID(int id)
        {
            return await ProfesorRepos.GetByID(id); //Devuelve un profesor de acuerdo al Usuario.ID que se le pasa (usuario vinculado)
        }
    }
}