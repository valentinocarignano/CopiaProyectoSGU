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

        public async Task<AsistenciaDTO> ActualizarAsistencia(string dnialumno, string materia, int año, int mes, int dia, bool estado)
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
            var asistenciaExistente = (await _asistenciaRepository.FindByConditionAsync(a => a.Fecha.Year == año && a.Fecha.Month == mes && a.Fecha.Day == dia && a.IdInscripcion == inscripcion.ID)).SingleOrDefault();
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

        public async Task<List<AsistenciaDTO>> ObtenerAsistenciasPorMateria(int idMateria)
        {
            try
            {
                List<Asistencia> listaAsistencia = (await _asistenciaRepository.FindByConditionAsync(a => a. == idMateria)).ToList();

                //HashSet<int> mejora mucho el rendimiento de busqueda en comparacion con List<int>
                HashSet<int> idsExamenes = listaExamenes.Select(e => e.ID).ToHashSet();

                List<NotaAlumno> listaNotas = (await _notaAlumnoRepository.FindAllAsync()).Where(n => idsExamenes.Contains(n.IdExamen)).ToList();

                if (listaNotas == null || listaNotas.Count == 0)
                {
                    return new List<NotaAlumnoDTO>();
                }

                List<AsistenciaDTO> listaAsistenciasDTO = listaAsistencias.Select(t => new AsistenciaDTO
                {
                    ID = t.ID,
                    DniAlumno = t.Inscripcion.Alumno.Usuario.DNI,
                    Materia = t.materia,
                    Dia = t.,
                    Horario = horarioEntidad.Descripcion,
                    Estado = asistenciaExistente.Estado,
                    Fecha = asistenciaExistente.Fecha
                }).ToList();

                return listaUsuariosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
            ;
        }

        //public async Task<List<Asistencia>> ObtenerAsistencias()
        //{
        //    return await _asistenciaRepository.GetAll();
        //}

        //public IEnumerable<Asistencia> ObtenerAsistenciasPorFecha(DateTime fecha)
        //{
        //    return _asistenciaRepository.FindByCondition(a => a.Fecha.Date == fecha.Date);
        //}

    }
}

