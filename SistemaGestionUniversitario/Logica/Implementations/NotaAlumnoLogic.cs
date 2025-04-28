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

        public void AltaNotaAlumno(int nota, int idAlumno, int idExamen)
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

        public async Task<List<NotaAlumno>> ObtenerNotas()
        {
            return await _notaAlumnoRepository.GetAll();
        }

        public void BajaNotaAlumno(int idAlumno, int idExamen)
        {
            NotaAlumno? notaAlumnoEliminar = _notaAlumnoRepository.FindByCondition(p => p.IdAlumno == idAlumno && p.IdExamen == idExamen).FirstOrDefault();

            if (notaAlumnoEliminar == null)
            {
                throw new InvalidOperationException("La nota que se desea eliminar no existe.");
            }

            _notaAlumnoRepository.Delete(notaAlumnoEliminar);
            _notaAlumnoRepository.Save();
        }

        public void ActualizacionNotaAlumno(int nota, int idAlumno, int idExamen)
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
    }
}
