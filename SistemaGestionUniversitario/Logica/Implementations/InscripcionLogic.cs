using Datos.Repositories.Contracts;
using Datos.Repositories.Implementations;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class InscripcionLogic : IInscripcionLogic
    {
        private IInscripcionRepository _inscripcionRepository;

        public InscripcionLogic(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task AltaInscripcion(string IdAlumno, string IdMateria)
        {
            if (!Int32.TryParse(IdAlumno, out int IdAlumnoParse))
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a un alumno o el ID no es valido.");
            }

            if (!Int32.TryParse(IdMateria, out int IdMateriaParse))
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a una materia o el ID no es valido.");
            }

            Inscripcion? inscripcionExistente = (await _inscripcionRepository.FindByConditionAsync(p => p.IdMateria == IdMateriaParse && p.IdAlumno == IdAlumnoParse)).FirstOrDefault();

            if (inscripcionExistente != null)
            {
                throw new ArgumentNullException("El alumno ya esta inscripto a esta materia.");
            }

            Inscripcion inscripcionNueva = new Inscripcion()
            {
                IdAlumno = IdAlumnoParse,
                IdMateria = IdMateriaParse
            };

            await _inscripcionRepository.AddAsync(inscripcionNueva);
            await _inscripcionRepository.SaveAsync();
        }
        public async Task BajaInscripcion(string IdMateria, string IdAlumno)
        {
            if (!Int32.TryParse(IdAlumno, out int IdAlumnoParse))
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a un alumno o el ID no es valido.");
            }

            if (!Int32.TryParse(IdMateria, out int IdMateriaParse))
            {
                throw new ArgumentNullException("La inscripcion debe estar vinculada a una materia o el ID no es valido.");
            }

            Inscripcion? inscripcionEliminar = (await _inscripcionRepository.FindByConditionAsync(p => p.IdMateria == IdMateriaParse && p.IdAlumno == IdAlumnoParse)).FirstOrDefault();

            if (inscripcionEliminar == null)
            {
                throw new InvalidOperationException("La inscripcion que se desea dar de baja no existe.");
            }

            _inscripcionRepository.Remove(inscripcionEliminar);
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
