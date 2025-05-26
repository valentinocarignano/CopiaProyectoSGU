using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
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
        public async Task ActualizacionAlumno(Usuario usuario)
        {
            Alumno? alumnoExistente = (await _alumnoRepository.FindByConditionAsync(p => p.Usuario == usuario)).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentException("El usuario ingresado no esta vinculado con ningun alumno.");
            }

            _alumnoRepository.Update(alumnoExistente);
            await _alumnoRepository.SaveAsync();
        }
        public async Task<List<AlumnoDTO>> ObtenerAlumnos()
        {
            try
            {
                List<Alumno> listaAlumnos = (await _alumnoRepository.FindAllAsync()).ToList();

                if (listaAlumnos == null)
                {
                    return null;
                }

                List<AlumnoDTO> listaAlumnosDTO = listaAlumnos.Select(t => new AlumnoDTO
                {
                    ID = t.ID,
                    DNI = t.Usuario.DNI,
                    Nombre = t.Usuario.Nombre,
                    Apellido = t.Usuario.Apellido,
                    FechaIngreso = t.FechaIngreso,
                }).ToList();

                return listaAlumnosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<AlumnoDTO> ObtenerAlumnoDNI(string dni)
        {
            if (!ValidacionesCampos.DocumentoEsValido(dni))
            {
                throw new ArgumentException("El DNI ingresado no es valido.");
            }

            Alumno? alumno = (await _alumnoRepository.FindByConditionAsync(t => t.Usuario.DNI == dni)).FirstOrDefault();

            if (alumno == null)
            {
                return null;
            }

            AlumnoDTO alumnoDTO = new AlumnoDTO()
            {
                ID = alumno.ID,
                DNI = alumno.Usuario.DNI,
                Nombre = alumno.Usuario.Nombre,
                Apellido = alumno.Usuario.Apellido,
                FechaIngreso = alumno.FechaIngreso,
            };

            return alumnoDTO;
        }
    }
}