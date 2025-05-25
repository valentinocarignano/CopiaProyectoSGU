using Logica.Contracts;
using Datos.Repositories.Contracts;
using Entidades.Entities;
using Entidades.DTOs.Respuestas;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            if (fecha == default)
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
        public async Task<AsistenciaDTO> ActualizarAsistencia(string dnialumno, string materia, int ano, int mes, int dia, bool estado)
        {
            if (string.IsNullOrEmpty(dnialumno))
                throw new ArgumentNullException(nameof(dnialumno));
            if (string.IsNullOrEmpty(materia))
                throw new ArgumentNullException(nameof(materia));

            // Buscar alumno por DNI
            var alumno = (await _alumnoRepository.FindByConditionAsync(a => a.Usuario.DNI == dnialumno)).SingleOrDefault();
            if (alumno == null)
                throw new InvalidOperationException("No se encontró un alumno con ese DNI.");

            // Buscar materia por nombre
            var materiaEntidad = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == materia)).SingleOrDefault();
            if (materiaEntidad == null)
                throw new InvalidOperationException("No se encontró la materia.");

            // Buscar inscripción
            var inscripcion = (await _inscripcionRepository.FindByConditionAsync(i => i.IdAlumno == alumno.ID && i.IdMateria == materiaEntidad.ID)).SingleOrDefault();
            if (inscripcion == null)
                throw new InvalidOperationException("El alumno no está inscripto en esa materia.");

            // Buscar asistencia por fecha exacta e inscripción
            var asistenciaExistente = (await _asistenciaRepository.FindByConditionAsync(a => a.Fecha.Year == ano && a.Fecha.Month == mes && a.Fecha.Day == dia && a.IdInscripcion == inscripcion.ID)).SingleOrDefault();
            if (asistenciaExistente == null)
                throw new InvalidOperationException("No se encontró la asistencia para ese día.");

            // Actualizar estado
            asistenciaExistente.Estado = estado;
            _asistenciaRepository.Update(asistenciaExistente);
            await _asistenciaRepository.SaveAsync();

            // Obtener información relacionada
            var diaHorarioMateria = (await _diaHorarioMateriaRepository.FindByConditionAsync(dhm => dhm.ID == asistenciaExistente.IdDiaHorarioMateria)).SingleOrDefault();
            if (diaHorarioMateria == null)
                throw new InvalidOperationException("No se encontró el registro de día/horario de la materia.");

            var diaHorario = (await _diaHorarioRepository.FindByConditionAsync(dh => dh.ID == diaHorarioMateria.IdDiaHorario)).SingleOrDefault();
            if (diaHorario == null)
                throw new InvalidOperationException("No se encontró la combinación de día y horario.");

            var diaEntidad = (await _diaRepository.FindByConditionAsync(d => d.ID == diaHorario.IdDia)).SingleOrDefault();

            var horarioEntidad = (await _horarioRepository.FindByConditionAsync(h => h.ID == diaHorario.IdHorario)).SingleOrDefault();
            if (diaEntidad == null || horarioEntidad == null)
                throw new InvalidOperationException("No se encontró el día u horario correspondiente.");

            // Construir DTO actualizado
            var asistenciaDTO = new AsistenciaDTO
            {
                ID = asistenciaExistente.ID,
                DniAlumno = dnialumno,
                Materia = materia,
                Dia = diaEntidad.Descripcion,
                Horario = horarioEntidad.Descripcion,
                Estado = asistenciaExistente.Estado,
                Fecha = asistenciaExistente.Fecha
            };

            return asistenciaDTO;
        }
        public async Task EliminarAsistencia(string dnialumno, string nombremateria, int ano, int mes, int dia)
        {
            var alumno = (await _alumnoRepository.FindByConditionAsync(a => a.Usuario.DNI == dnialumno)).SingleOrDefault();
            if (alumno == null)
                throw new InvalidOperationException("No se encontró un alumno con ese DNI.");

            var materiaEntidad = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombremateria)).SingleOrDefault();
            if (materiaEntidad == null)
                throw new InvalidOperationException("No se encontró la materia.");

            var inscripcion = (await _inscripcionRepository.FindByConditionAsync(i => i.IdAlumno == alumno.ID && i.IdMateria == materiaEntidad.ID)).SingleOrDefault();
            if (inscripcion == null)
                throw new InvalidOperationException("El alumno no está inscripto en esa materia.");

            var asistencia = (await _asistenciaRepository.FindByConditionAsync(a => a.Fecha.Year == ano && a.Fecha.Month == mes && a.Fecha.Day == dia && a.IdInscripcion == inscripcion.ID)).SingleOrDefault();
            if (asistencia == null)
                
            throw new InvalidOperationException("No se encontró la asistencia para ese día.");
            _asistenciaRepository.Remove(asistencia);
            await _asistenciaRepository.SaveAsync();
        }
        public async Task<List<AsistenciaDTO>> ObtenerAsistenciasPorMateria(string nombreMateria)
        {
            // 1. Buscar la materia
            var materia = (await _materiaRepository.FindByConditionAsync(m => m.Nombre == nombreMateria)).SingleOrDefault();
            if (materia == null)
                throw new InvalidOperationException("No se encontró la materia.");

            // 2. Buscar inscripciones de esa materia
            var inscripciones = (await _inscripcionRepository.FindByConditionAsync(i => i.IdMateria == materia.ID)).ToList();
            if (!inscripciones.Any())
                return new List<AsistenciaDTO>();

            // 3. Obtener todos los IDs de inscripciones
            var idsInscripciones = inscripciones.Select(i => i.ID).ToHashSet();

            // 4. Buscar todas las asistencias que correspondan a esas inscripciones
            var asistencias = (await _asistenciaRepository.FindAllAsync()).Where(a => idsInscripciones.Contains(a.IdInscripcion)).ToList();

            List<AsistenciaDTO> resultado = new();

            foreach (var asistencia in asistencias)
            {
                // Obtener datos relacionados

                var inscripcion = inscripciones.First(i => i.ID == asistencia.IdInscripcion);

                var alumno = (await _alumnoRepository.FindByConditionAsync(a => a.ID == inscripcion.IdAlumno)).FirstOrDefault();
                if (alumno == null || alumno.Usuario == null)
                    continue;

                var diaHorarioMateria = (await _diaHorarioMateriaRepository.FindByConditionAsync(dhm => dhm.ID == asistencia.IdDiaHorarioMateria)).FirstOrDefault();
                var diaHorario = diaHorarioMateria != null
                    ? (await _diaHorarioRepository.FindByConditionAsync(dh => dh.ID == diaHorarioMateria.IdDiaHorario)).FirstOrDefault()
                    : null;

                var dia = diaHorario != null
                    ? (await _diaRepository.FindByConditionAsync(d => d.ID == diaHorario.IdDia)).FirstOrDefault()
                    : null;

                var horario = diaHorario != null
                    ? (await _horarioRepository.FindByConditionAsync(h => h.ID == diaHorario.IdHorario)).FirstOrDefault()
                    : null;

                // Crear DTO
                var dto = new AsistenciaDTO
                {
                    ID = asistencia.ID,
                    DniAlumno = alumno.Usuario.DNI,
                    Materia = materia.Nombre,
                    Dia = dia?.Descripcion,
                    Horario = horario?.Descripcion,
                    Estado = asistencia.Estado,
                    Fecha = asistencia.Fecha
                };

                resultado.Add(dto);
            }

            return resultado;
        }
    }
}