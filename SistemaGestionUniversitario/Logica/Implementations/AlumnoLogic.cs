using Datos.Repositories.Contracts;
using Datos.Repositories.Implementations;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class AlumnoLogic : IAlumnoLogic
    {
        private IAlumnoRepository _alumnoRepository;

        public AlumnoLogic(IAlumnoRepository alumnoRepository)
        {
            _alumnoRepository = alumnoRepository;
        }

        public async Task AltaAlumno(Usuario usuario, DateTime? fechaIngreso)
        {


            if (fechaIngreso == null)
            {
                throw new ArgumentNullException("Se debe asignar una fecha de inicio de ingreso.");
            }

            if (usuario == null)
            {
                throw new ArgumentNullException("El alumno debe estar vinculado a un usuario.");
            }

            Alumno alumnoNuevo = new Alumno()
            {
                FechaIngreso = fechaIngreso.Value,
                Usuario = usuario,
            };

            await _alumnoRepository.AddAsync(alumnoNuevo);
            await _alumnoRepository.SaveAsync();
        }
        public async Task BajaAlumno(string documento)
        {
            if (string.IsNullOrEmpty(documento) || !ValidacionesCampos.DocumentoEsValido(documento))
            {
                throw new ArgumentException("El documento ingresado no es valido.");
            }

            Alumno? alumnoExistente = (await _alumnoRepository.FindByConditionAsync(t => t.Usuario.DNI == documento)).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentException("No hay ningun alumno vinculado con el documento ingresado..");
            }

            _alumnoRepository.Remove(alumnoExistente);
            await _alumnoRepository.SaveAsync();
        }

        public async Task ActualizacionAlumno(string documento, ModificarAlumnoDTO alumnoActualizar)
        {
            if (alumnoActualizar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun profesor.");
            }

            if (alumnoActualizar.Usuario == null)
            {
                throw new ArgumentNullException("El alumno debe estar vinculado a un usuario.");
            }

            await AlumnoRepos.Update(alumnoActualizar.Usuario.DNI, alumnoActualizar);
        }

        public async Task<Alumno?> ObtenerAlumnoID(int id)
        {
            return await AlumnoRepos.GetByID(id); //Devuelve un alumno de acuerdo al Usuario.ID que se le pasa (usuario vinculado)
        }
    }
}