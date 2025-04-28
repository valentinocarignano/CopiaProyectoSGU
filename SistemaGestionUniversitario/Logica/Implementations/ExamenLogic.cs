using Datos.Repositories.Contracts;
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
    public class ExamenLogic : IExamenLogic
    {
        private IExamenRepository _examenRepository;

        public ExamenLogic(IExamenRepository examenRepository)
        {
            _examenRepository = examenRepository;
        }

        public void AltaExamen(Examen examenAgregar)
        {
            Examen examenNuevo = new Examen();

            if (examenAgregar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun examen.");
            }

            if (examenAgregar.Materia == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a una materia.");
            }

            if (examenAgregar.DiaHorario == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a un dia y horario.");
            }

            Examen? examenExistente = _examenRepository.FindByCondition(p => p.Materia == examenAgregar.Materia && p.DiaHorario == examenAgregar.DiaHorario).FirstOrDefault();
            
            if (examenExistente != null)
            {
                throw new InvalidOperationException("Ya existe un examen de la materia seleccionada en el dia y hora ingresado.");
            }

            examenNuevo.Tipo = examenAgregar.Tipo;
            examenNuevo.Fecha = examenAgregar.Fecha;
            examenNuevo.Materia = examenAgregar.Materia;
            examenNuevo.DiaHorario = examenAgregar.DiaHorario;

            _examenRepository.Create(examenNuevo);
            _examenRepository.Save();
        }
        public void ActualizacionExamen(int idMateria, int idDiaHorario, Examen examenActualizar)
        {            
            if (examenActualizar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun examen.");
            }
            
            if (examenActualizar.Materia == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a una materia.");
            }
            
            if (examenActualizar.DiaHorario == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a un dia y horario.");
            }

            Examen? examenExistente = _examenRepository.FindByCondition(e => e.Materia.ID == idMateria && e.DiaHorario.ID == idDiaHorario).FirstOrDefault();
            
            if (examenExistente == null)
            {
                throw new InvalidOperationException("El examen que se quiere actualizar no existe.");
            }

            examenExistente.Fecha = examenActualizar.Fecha;
            examenExistente.DiaHorario = examenActualizar.DiaHorario;

            _examenRepository.Update(examenExistente);
            _examenRepository.Save();
        }
        public void BajaExamen(int idMateria, int idDiaHorario)
        {
            Examen? examenEliminar = _examenRepository.FindByCondition(p => p.Materia.ID == idMateria && p.DiaHorario.ID == idDiaHorario).FirstOrDefault();

            if (examenEliminar == null)
            {
                throw new InvalidOperationException("El examen que se desea eliminar no existe.");
            }

            _examenRepository.Delete(examenEliminar);
            _examenRepository.Save();
        }
        public async Task<List<Examen>> ObtenerExamenes()
        {
            return await _examenRepository.GetAll();
        }
    }
}