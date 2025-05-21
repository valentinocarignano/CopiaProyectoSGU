using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaAlumnoController : ControllerBase
    {
        private readonly INotaAlumnoLogic _notaAlumnoLogic;

        public NotaAlumnoController(INotaAlumnoLogic notaAlumnoLogic)
        {
            _notaAlumnoLogic = notaAlumnoLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerNotas()
        {
            List<NotaAlumnoDTO> notaAlumnoDTO = await _notaAlumnoLogic.ObtenerNotas();

            return Ok(notaAlumnoDTO);
        }

        [HttpGet("id-materia/{id-materia}")]
        public async Task<IActionResult> ObtenerNotasPorMateria(int idMateria)
        {
            List<NotaAlumnoDTO> notaAlumnoDTO = await _notaAlumnoLogic.ObtenerNotasPorMateria(idMateria);

            return Ok(notaAlumnoDTO);
        }

        [HttpGet("id-alumno/{id-alumno}")]
        public async Task<IActionResult> ObtenerNotasPorAlumno(int idAlumno)
        {
            List<NotaAlumnoDTO> notaAlumnoDTO = await _notaAlumnoLogic.ObtenerNotasPorAlumno(idAlumno);

            return Ok(notaAlumnoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CrearNotaAlumno([FromBody] CrearNotaAlumnoDTO crearNotaAlumnoDTO)
        {
            await _notaAlumnoLogic.AltaNotaAlumno(crearNotaAlumnoDTO.Nota, crearNotaAlumnoDTO.IDAlumno, crearNotaAlumnoDTO.IDExamen);

            return Ok();
        }

        [HttpPut("id-alumno/{id-alumno}/id-examen/{id-examen}")]
        public async Task<IActionResult> ModificarNotaAlumno(int idAlumno, int idExamen, [FromBody] ModificarNotaAlumnoDTO modificarNotaAlumnoDTO)
        {
            NotaAlumnoDTO notaAlumnoDTO = await _notaAlumnoLogic.ActualizacionNotaAlumno(
                idAlumno,
                idExamen,
                modificarNotaAlumnoDTO.Nota);

            if (notaAlumnoDTO == null)
            {
                return NotFound();
            }

            return Ok(notaAlumnoDTO);
        }

        [HttpDelete("id-alumno/{id-alumno}/id-examen/{id-examen}")]
        public async Task<IActionResult> EliminarNotaAlumno(int idAlumno, int idExamen)
        {
            try
            {
                await _notaAlumnoLogic.BajaNotaAlumno(idAlumno, idExamen);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}