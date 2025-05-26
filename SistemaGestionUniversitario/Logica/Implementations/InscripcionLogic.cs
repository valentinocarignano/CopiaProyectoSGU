using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class InscripcionLogic : IInscripcionLogic
    {
        private IInscripcionRepository _inscripcionRepository;
        private IMateriaRepository _materiaRepository;
        private IAlumnoRepository _alumnoRepository;

        public InscripcionLogic(IInscripcionRepository inscripcionRepository, IMateriaRepository materiaRepository, IAlumnoRepository alumnoRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _materiaRepository = materiaRepository;
            _alumnoRepository = alumnoRepository;
        }

        public async Task AltaInscripcion(string dniAlumno, string nombreMateria)
        {
            Alumno? alumnoExistente = (await _alumnoRepository.FindByConditionAsync(p => p.Usuario.DNI == dniAlumno)).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a un alumno con DNI valido.");
            }

            Materia? materiaExistente = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombreMateria)).FirstOrDefault();
            if (materiaExistente == null)
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a una materia con nombre valido.");
            }

            Inscripcion? inscripcionExistente = (await _inscripcionRepository.FindByConditionAsync(p => p.IdMateria == materiaExistente.ID && p.IdAlumno == alumnoExistente.ID)).FirstOrDefault();

            if (inscripcionExistente != null)
            {
                throw new ArgumentNullException("El alumno ya esta inscripto a esta materia.");
            }

            Inscripcion inscripcionNueva = new Inscripcion()
            {
                IdAlumno = alumnoExistente.ID,
                IdMateria = materiaExistente.ID
            };

            await _inscripcionRepository.AddAsync(inscripcionNueva);
            await _inscripcionRepository.SaveAsync();
        }
        public async Task BajaInscripcion(string dniAlumno, string nombreMateria)
        {
            Alumno? alumnoExistente = (await _alumnoRepository.FindByConditionAsync(p => p.Usuario.DNI == dniAlumno)).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a un alumno con DNI valido.");
            }

            Materia? materiaExistente = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombreMateria)).FirstOrDefault();
            if (materiaExistente == null)
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a una materia con nombre valido.");
            }

            Inscripcion? inscripcionExistente = (await _inscripcionRepository.FindByConditionAsync(p => p.IdMateria == materiaExistente.ID && p.IdAlumno == alumnoExistente.ID)).FirstOrDefault();

            if (inscripcionExistente == null)
            {
                throw new InvalidOperationException("La inscripcion que se desea dar de baja no existe.");
            }

            _inscripcionRepository.Remove(inscripcionExistente);
            await _inscripcionRepository.SaveAsync();
        }
        public async Task<List<InscripcionDTO>> ObtenerInscripciones()
        {
            try
            {
                List<Inscripcion> listaInscripciones = (await _inscripcionRepository.FindAllAsync()).ToList();

                if (listaInscripciones == null)
                {
                    return null;
                }

                List<InscripcionDTO> listaInscripcionesDTO = listaInscripciones.Select(t => new InscripcionDTO
                {
                    ID = t.ID,
                    IdAlumno = t.IdAlumno,
                    IdMateria = t.IdMateria
                }).ToList();

                return listaInscripcionesDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
    }
}