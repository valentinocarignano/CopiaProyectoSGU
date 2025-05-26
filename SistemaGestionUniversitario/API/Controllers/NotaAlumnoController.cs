using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
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

        [HttpGet("nombreMateria/{nombreMateria}")]
        public async Task<IActionResult> ObtenerNotasPorMateria(string nombreMateria)
        {
            List<NotaAlumnoDTO> notaAlumnoDTO = await _notaAlumnoLogic.ObtenerNotasPorMateria(nombreMateria);

            return Ok(notaAlumnoDTO);
        }

        [HttpGet("dniAlumno/{dniAlumno}")]
        public async Task<IActionResult> ObtenerNotasPorAlumno(string dniAlumno)
        {
            List<NotaAlumnoDTO> notaAlumnoDTO = await _notaAlumnoLogic.ObtenerNotasPorAlumno(dniAlumno);

            return Ok(notaAlumnoDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CrearNotaAlumno([FromBody] CrearNotaAlumnoDTO crearNotaAlumnoDTO)
        {
            await _notaAlumnoLogic.AltaNotaAlumno(crearNotaAlumnoDTO.Nota, crearNotaAlumnoDTO.DNIAlumno, crearNotaAlumnoDTO.IDExamen);

            return Ok();
        }

        [HttpPut("{dniAlumno}/{idExamen}")]
        public async Task<IActionResult> ModificarNotaAlumno(string dniAlumno, int idExamen, [FromBody] ModificarNotaAlumnoDTO modificarNotaAlumnoDTO)
        {
            NotaAlumnoDTO notaAlumnoDTO = await _notaAlumnoLogic.ActualizacionNotaAlumno(
                modificarNotaAlumnoDTO.Nota,
                dniAlumno,
                idExamen);

            if (notaAlumnoDTO == null)
            {
                return NotFound();
            }

            return Ok(notaAlumnoDTO);
        }

        [HttpDelete("{dniAlumno}/{idExamen}")]
        public async Task<IActionResult> EliminarNotaAlumno(string dniAlumno, int idExamen)
        {
            try
            {
                await _notaAlumnoLogic.BajaNotaAlumno(dniAlumno, idExamen);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}