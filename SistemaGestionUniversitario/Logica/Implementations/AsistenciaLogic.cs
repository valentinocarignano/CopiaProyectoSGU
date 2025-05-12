using Logica.Contracts;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Entidades.DTOs;
using Datos.Repositories.Implementations;

namespace Logica.Implementations
{
    public class AsistenciaLogic : IAsistenciaLogic
    {
        IAsistenciaRepository _asistenciaRepository;
        IAlumnoRepository _alumnoRepository;
        IMateriaRepository _materiaRepository;
        IInscripcionRepository _inscripcionRepository;
        IDiaHorarioRepository _diaHorarioRepository;
        IDiaRepository _diaRepository;
        IHorarioRepository _horarioRepository;
        IDiaHorarioMateriaRepository _diaHorarioMateriaRepository;


        public AsistenciaLogic(IAsistenciaRepository asistenciaRepository, IAlumnoRepository alumnoRepository, IMateriaRepository materiaRepository, IInscripcionRepository inscripcionRepository, IDiaHorarioRepository diaHorarioRepository, IDiaRepository diaRepository, IHorarioRepository horarioRepository, IDiaHorarioMateriaRepository diaHorarioMateriaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
            _alumnoRepository = alumnoRepository;
            _materiaRepository = materiaRepository;
            _inscripcionRepository = inscripcionRepository;
            _diaHorarioRepository = diaHorarioRepository;
            _diaRepository = diaRepository;
            _horarioRepository = horarioRepository;
            _diaHorarioMateriaRepository = diaHorarioMateriaRepository;
            
        }

        public async Task AltaAsistencia(int idinscripcion, int iddiahorariomateria, bool estado, DateTime fecha)
        {
            List<string> camposErroneos = new List<string>();

            if (idinscripcion == null)
            {
                camposErroneos.Add("id");
            }

            if (iddiahorariomateria == null)
            {
                camposErroneos.Add("id DiaHorarioMateria");
            }

            if (estado == null)
            {
                camposErroneos.Add("Estado");
            }

            if (fecha == null)
            {
                camposErroneos.Add("Fecha");
            }
            if (camposErroneos.Count > 0)
            {
                throw new ArgumentException("Los siguientes campos son inválidos: ", string.Join(", ", camposErroneos));
            }

            Asistencia? asistenciaExistente = (await _asistenciaRepository.FindByConditionAsync(p => p.IdDiaHorarioMateria == iddiahorariomateria && p.IdInscripcion == idinscripcion)).FirstOrDefault();

            if (asistenciaExistente != null)
            {
                throw new InvalidOperationException("Ya existe una asistencia registrada para la misma materia, día y hora.");
            }

            Asistencia asistenciaNueva = new Asistencia()
            {
                IdInscripcion = idinscripcion,
                IdDiaHorarioMateria = iddiahorariomateria,
                Estado = estado,
                Fecha = fecha,
            };
            await _asistenciaRepository.AddAsync(asistenciaNueva);
            await _asistenciaRepository.SaveAsync();
        }

        public async Task<AsistenciaDTO> ActualizarAsistencia(int idasistencia, bool estado, string dia, string horario, string materia, string dnialumno, DateTime fecha)
        {

            if (idasistencia == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna asistencia.");
            }

            if (dia == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun dia");
            }

            if (horario == null)
            {
                throw new ArgumentNullException("No se ha ingresado ningun horario.");
            }
            
            if(materia == null)
            {
                throw new ArgumentNullException("No se ha ingresado ninguna materia.");
            }

            var alumnos = await _alumnoRepository.FindByConditionAsync(a => (a.Usuario.DNI) == dnialumno);
            var alumno = alumnos.SingleOrDefault();

            if (alumno == null)
                throw new InvalidOperationException("No se encontró un alumno con ese nombre.");

            var materias = await _materiaRepository.FindByConditionAsync(m => m.Nombre == materia);
            var materiaEntidad = materias.SingleOrDefault();
            if (materiaEntidad == null)
                throw new InvalidOperationException("No se encontró la materia.");

            var inscripciones = await _inscripcionRepository.FindByConditionAsync(i => i.IdAlumno == alumno.ID && i.IdMateria == materiaEntidad.ID);
            var inscripcion = inscripciones.SingleOrDefault();

            if (inscripcion == null)
                throw new InvalidOperationException("El alumno no está inscripto en esa materia.");

            var dias = await _diaRepository.FindByConditionAsync(d => d.Descripcion == dia);
            var diaEntidad = dias.SingleOrDefault();

            var horarios = await _horarioRepository.FindByConditionAsync(h => h.Descripcion == horario);
            var horarioEntidad = horarios.SingleOrDefault();

            if (dia == null || horario == null)
                throw new InvalidOperationException("No se encontró el día u horario indicado.");

            var diaHorarios = await _diaHorarioRepository.FindByConditionAsync(dh => dh.IdDia == diaEntidad.ID && dh.IdHorario == horarioEntidad.ID);
            var diaHorario = diaHorarios.SingleOrDefault();

            if (diaHorario == null)
                throw new InvalidOperationException("No se encontró la combinación de día y horario.");

            var diaHorarioMateria = await _diaHorarioMateriaRepository.FindByConditionAsync(dhm => dhm.IdMateria == materiaEntidad.ID && dhm.IdDiaHorario == diaHorario.ID);
            var diaHorarioMateriaEntidad = diaHorarioMateria.SingleOrDefault();
            if (diaHorarioMateria == null)
                throw new InvalidOperationException("No se encontró la asignación de día/horario a esa materia.");

            var asistencia = await _asistenciaRepository.FindByConditionAsync(a => a.IdInscripcion == inscripcion.ID && a.IdDiaHorarioMateria == diaHorarioMateriaEntidad.ID);
            Asistencia? asistenciaExistente = asistencia.SingleOrDefault();
            if (asistenciaExistente == null)
            {
                throw new InvalidOperationException("La asistencia que se quiere actualizar no existe.");
            }

            asistenciaExistente.Estado = estado;

            _asistenciaRepository.Update(asistenciaExistente);
            _asistenciaRepository.SaveAsync();

            AsistenciaDTO asistenciaActualizadaDTO = new AsistenciaDTO()
            {
                ID = asistenciaExistente.ID,
                DniAlumno = dnialumno,
                Dia = dia,
                Horario = horario,
                Materia = materia,
                Estado = asistenciaExistente.Estado,
                Fecha = fecha
            };
            return asistenciaActualizadaDTO;
        }
        public async Task EliminarAsistencia(string dnialumno, string nombremateria, int año, int mes, int dia)
        {
            var alumnos = await _alumnoRepository.FindByConditionAsync(a => (a.Usuario.DNI) == dnialumno);
            var alumno = alumnos.SingleOrDefault();
            if (alumno == null)
                throw new InvalidOperationException("No se encontró un alumno con ese nombre.");

            var materias = await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombremateria);
            var materiaEntidad = materias.SingleOrDefault();
            if (materiaEntidad == null)
                throw new InvalidOperationException("No se encontró la materia.");

            var inscripciones = await _inscripcionRepository.FindByConditionAsync(i => i.IdAlumno == alumno.ID && i.IdMateria == materiaEntidad.ID);
            var inscripcion = inscripciones.SingleOrDefault();

            if (inscripcion == null)
                throw new InvalidOperationException("El alumno no está inscripto en esa materia.");

            var asistencias = await _asistenciaRepository.FindByConditionAsync(a => a.Fecha.Year == año && a.Fecha.Month == mes && a.Fecha.Day == dia && a.IdInscripcion == inscripcion.ID);
            var asistencia = asistencias.SingleOrDefault();
            if (asistencia == null)
                throw new InvalidOperationException("No se encontró la asistencia para ese día.");
            _asistenciaRepository.Remove(asistencia);
            await _asistenciaRepository.SaveAsync();
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

