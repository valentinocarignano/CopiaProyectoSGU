using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaLogic _asistenciaLogic;

        public AsistenciaController(IAsistenciaLogic asistenciaLogic)
        {
            _asistenciaLogic = asistenciaLogic;
        }

        [HttpGet("nombreMateria/{nombreMateria}")]
        public async Task<IActionResult> ObtenerAsistenciasPorMateria(string nombreMateria)
        {
            List<AsistenciaDTO> asistenciaDTO = await _asistenciaLogic.ObtenerAsistenciasPorMateria(nombreMateria);

            return Ok(asistenciaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearAsistenciaDTO crearAsistenciaDTO)
        {
            await _asistenciaLogic.AltaAsistencia(crearAsistenciaDTO.idInscripcion, crearAsistenciaDTO.idDiaHorarioMateria, crearAsistenciaDTO.Estado, crearAsistenciaDTO.Fecha);

            return Ok();
        }

        [HttpPut("dniAlumno/{dniAlumno}/materia/{materia}/año{ano}/mes{mes}/dia{dia}")]
        public async Task<IActionResult> Modificar(string dniAlumno, string materia, int ano, int mes, int dia, [FromBody] ModificarAsistenciaDTO modificarAsistenciaDTO)
        {
            AsistenciaDTO asistenciaDTO = await _asistenciaLogic.ActualizarAsistencia(dniAlumno,materia,ano,mes, dia, modificarAsistenciaDTO.Estado);

            if (asistenciaDTO == null)
            {
                return NotFound();
            }

            return Ok(asistenciaDTO);
        }

        [HttpDelete("dniAlumno/{dniAlumno}/materia/{materia}/año{ano}/mes{mes}/dia{dia}")]
        public async Task<IActionResult> Eliminar(string dnialumno, string materia, int ano, int mes, int dia)
        {
            try
            {
                await _asistenciaLogic.EliminarAsistencia(dnialumno, materia, ano, mes, dia);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        

    }
}
