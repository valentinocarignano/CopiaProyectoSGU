using Datos.Repositories.Contracts;
using Datos.Repositories.Implementations;
using Entidades.DTOs;
using Entidades.Entities;
using Logica.Contracts;
using Negocio.Contracts;
using Shared.Entities;
using Shared.Repositories;
using Shared.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.Implementations
{
    public class NotaAlumnoLogic : INotaAlumnoLogic
    {
        private INotaAlumnoRepository _notaAlumnoRepository;
        private IAlumnoRepository _alumnoRepository;
        private IExamenRepository _examenRepository;

        public NotaAlumnoLogic(INotaAlumnoRepository notaAlumnoRepository, IAlumnoRepository alumnoRepository, IExamenRepository examenRepository)
        {
            _notaAlumnoRepository = notaAlumnoRepository;
            _alumnoRepository = alumnoRepository;
            _examenRepository = examenRepository;
        }

        public async Task AltaNotaAlumno(int nota, int idAlumno, int idExamen)
        {
            NotaAlumno notaAlumnoAgregar = new NotaAlumno()
            {
                Nota = nota,
                IdAlumno = idAlumno,
                IdExamen = idExamen,
            };

            if (notaAlumnoAgregar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna nota para un alumno.");
            }

            if (notaAlumnoAgregar.Nota < 0 || notaAlumnoAgregar.Nota > 10)
            {
                throw new ArgumentNullException("La nota ingresada debe tener un valor entre 0 y 10.");
            }

            Alumno? alumnoExistente = _alumnoRepository.FindByCondition(p => p.ID == notaAlumnoAgregar.IdAlumno).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentNullException("El alumno seleccionado no existe.");
            }

            Examen? examenExistente = _examenRepository.FindByCondition(p => p.ID == notaAlumnoAgregar.IdExamen).FirstOrDefault();

            if (examenExistente == null)
            {
                throw new ArgumentNullException("El examen seleccionado no existe.");
            }

            NotaAlumno? notaAlumnoExistente = _notaAlumnoRepository.FindByCondition(p => p.IdAlumno == notaAlumnoAgregar.IdAlumno && p.IdExamen == notaAlumnoAgregar.IdExamen).FirstOrDefault();

            if (notaAlumnoExistente != null)
            {
                throw new InvalidOperationException("Ya se le asigno una nota al alumno ingresado en el examen seleccionado.");
            }

            NotaAlumno notaAlumnoNueva = new NotaAlumno();

            notaAlumnoNueva.Nota = notaAlumnoAgregar.Nota;
            notaAlumnoNueva.IdAlumno = notaAlumnoAgregar.IdAlumno;
            notaAlumnoNueva.IdExamen = notaAlumnoAgregar.IdExamen;

            _notaAlumnoRepository.Create(notaAlumnoNueva);
            _notaAlumnoRepository.Save();
        }
        public async Task<NotaAlumnoDTO> ActualizacionNotaAlumno(int nota, int idAlumno, int idExamen)
        {
            NotaAlumno notaAlumnoActualizar = new NotaAlumno()
            {
                Nota = nota,
                IdAlumno = idAlumno,
                IdExamen = idExamen,
            };

            if (notaAlumnoActualizar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna nota para un alumno.");
            }

            if(notaAlumnoActualizar.Nota < 0 || notaAlumnoActualizar.Nota > 10)
            {
                throw new ArgumentNullException("La nota ingresada debe tener un valor entre 0 y 10.");
            }

            Alumno? alumnoExistente = _alumnoRepository.FindByCondition(p => p.ID == notaAlumnoActualizar.IdAlumno).FirstOrDefault();

            if (alumnoExistente == null)
            {
                throw new ArgumentNullException("El alumno seleccionado no existe.");
            }

            Examen? examenExistente = _examenRepository.FindByCondition(p => p.ID == notaAlumnoActualizar.IdExamen).FirstOrDefault();

            if (examenExistente == null)
            {
                throw new ArgumentNullException("El examen seleccionado no existe.");
            }

            NotaAlumno? notaAlumnoExistente = _notaAlumnoRepository.FindByCondition(p => p.IdAlumno == notaAlumnoActualizar.IdAlumno && p.IdExamen == notaAlumnoActualizar.IdExamen).FirstOrDefault();

            if (notaAlumnoExistente == null)
            {
                throw new InvalidOperationException("El alumno ingresado en el examen seleccionado no tiene ninguna nota para actualizar.");
            }

            notaAlumnoExistente.Nota = notaAlumnoActualizar.Nota;
            notaAlumnoExistente.IdAlumno = notaAlumnoActualizar.IdAlumno;
            notaAlumnoExistente.IdExamen = notaAlumnoActualizar.IdExamen;

            _notaAlumnoRepository.Update(notaAlumnoExistente);
            _notaAlumnoRepository.Save();
        }
        public async Task BajaNotaAlumno(int idAlumno, int idExamen)
        {
            NotaAlumno? notaAlumnoEliminar = _notaAlumnoRepository.FindByCondition(p => p.IdAlumno == idAlumno && p.IdExamen == idExamen).FirstOrDefault();

            if (notaAlumnoEliminar == null)
            {
                throw new InvalidOperationException("La nota que se desea eliminar no existe.");
            }

            _notaAlumnoRepository.Delete(notaAlumnoEliminar);
            _notaAlumnoRepository.Save();
        }
        public async Task<List<NotaAlumnoDTO>> ObtenerNotas()
        {
            try
            {
                List<NotaAlumno> listaNotas = (await _notaAlumnoRepository.FindAllAsync()).ToList();

                if (listaNotas == null)
                {
                    return null;
                }

                List<NotaAlumnoDTO> listaNotasDTO = new List<NotaAlumnoDTO>();
                foreach(NotaAlumno nota in listaNotas)
                {
                    List<string> listaNombresAlumnos = new List<string>();
                    foreach (int id in nota.IdAlumno)
                    {

                       listaDescripcionDiasHorarios.Add(await _alumnoRepository.FindByConditionAsync(a => a.ID == id).;
                    }

                    NotaAlumnoDTO notaDTO = new NotaAlumnoDTO()
                    {
                        ID = ,
                        AlumnoNombre =
                    }).ToList();

                    listaNotasDTO.Add(notaDTO);
                }

                return listaNotasDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<List<NotaAlumnoDTO>> ObtenerNotasPorMateria(int idMateria)
        {
            return await _notaAlumnoRepository.GetAll();
        }
        public async Task<List<NotaAlumnoDTO>> ObtenerNotasPorAlumno(int idAlumno)
        {
            return await _notaAlumnoRepository.GetAll();
        }
    }
}
