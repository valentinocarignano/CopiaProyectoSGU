using Microsoft.EntityFrameworkCore;
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
    public class AsistenciaLogic : IAsistenciaLogic
    {
        IAsistenciaRepository _asistenciaRepository;

        public AsistenciaLogic(IAsistenciaRepository asistenciaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
        }

        public void AltaAsistencia(Asistencia asistenciaAgregar)
        {
            Asistencia asistenciaNueva = new Asistencia();

            if (asistenciaAgregar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna asistencia.");
            }

            if (asistenciaAgregar.IdInscripcion == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a una inscripcion.");
            }

            if (asistenciaAgregar.IdDiaHorarioMateria == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a un dia y horario.");
            }

            if (asistenciaAgregar.Estado == null)
            {
                throw new ArgumentNullException("La asistencia debe tener un estado.");
            }

            if (asistenciaAgregar.Fecha == null)
            {
                throw new ArgumentNullException("La asistencia debe tener una fecha.");
            }

            Asistencia? asistenciaExistente = _asistenciaRepository.FindByCondition(p => p.IdDiaHorarioMateria == asistenciaAgregar.IdDiaHorarioMateria && p.IdInscripcion == asistenciaAgregar.IdInscripcion).FirstOrDefault();

            if (asistenciaExistente != null)
            {
                throw new InvalidOperationException("Ya existe una asistencia registrada para la misma materia, día y hora.");
            }

            asistenciaNueva.IdInscripcion = asistenciaAgregar.IdInscripcion;
            asistenciaNueva.IdDiaHorarioMateria = asistenciaAgregar.IdDiaHorarioMateria;
            asistenciaNueva.Estado = asistenciaAgregar.Estado;
            asistenciaNueva.Fecha = asistenciaAgregar.Fecha;

            _asistenciaRepository.Create(asistenciaNueva);
            _asistenciaRepository.Save();
        }

        public void ActualizarAsistencia(Asistencia asistenciaActualizar, int IdInscripcion)
        {

            if (asistenciaActualizar == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna asistencia.");
            }

            if (asistenciaActualizar.IdInscripcion == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a una inscripcion.");
            }

            if (asistenciaActualizar.IdDiaHorarioMateria == null)
            {
                throw new ArgumentNullException("El examen debe estar vinculado a un dia y horario.");
            }

            Asistencia? asistenciaExistente = _asistenciaRepository.FindByCondition(p => p.IdDiaHorarioMateria == asistenciaActualizar.IdDiaHorarioMateria && p.IdInscripcion == asistenciaActualizar.IdInscripcion).SingleOrDefault();
            if (asistenciaExistente == null)
            {
                throw new InvalidOperationException("La asistencia que se quiere actualizar no existe.");
            }

            asistenciaExistente.Estado = asistenciaActualizar.Estado;

            _asistenciaRepository.Update(asistenciaExistente);
            _asistenciaRepository.Save();
        }
        public void EliminarAsistencia(int año, int mes, int dia)
        {
            var fecha = new DateTime(año, mes, dia);

            if (fecha > DateTime.Now)
            {
                throw new ArgumentException("La fecha ingresada no es valido.");
            }

            Asistencia? asistenciaEliminar = _asistenciaRepository.FindByCondition(p => p.Fecha == fecha).FirstOrDefault();

            if (asistenciaEliminar == null)
            {
                throw new InvalidOperationException("La asistencia en esa fecha no existe.");
            }

            _asistenciaRepository.Delete(asistenciaEliminar);
            _asistenciaRepository.Save();

        }
        
        public async Task<List<Asistencia>> ObtenerAsistencias()
        {
            return await _asistenciaRepository.GetAll();
        }

        public IEnumerable<Asistencia> ObtenerAsistenciasPorFecha(DateTime fecha)
        {
            return _asistenciaRepository.FindByCondition(a => a.Fecha.Date == fecha.Date);
        }

    }
}

