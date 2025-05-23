using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamenController : ControllerBase
    {
        private readonly IExamenLogic _examenLogic;

        public ExamenController(IExamenLogic examenLogic)
        {
            _examenLogic = examenLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerExamenes()
        {
            List<ExamenDTO> examenDTO = await _examenLogic.ObtenerExamenes();

            return Ok(examenDTO);
        }

        [HttpGet("nombre-materia/{nombre-materia}")]
        public async Task<IActionResult> ObtenerExamenesPorMateria(string nombreMateria)
        {
            List<ExamenDTO> examenDTO = await _examenLogic.ObtenerExamenesPorMateria(nombreMateria);

            return Ok(examenDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearExamenDTO crearExamenDTO)
        {
            await _examenLogic.AltaExamen(crearExamenDTO.NombreMateria, crearExamenDTO.DescripcionDiaHorario, crearExamenDTO.TipoExamen);

            return Ok();
        }

        [HttpPut("id-materia/{id-materia}/id-diahorario/{id-diahorario}")]
        public async Task<IActionResult> Modificar(int idMateria, int idDiaHorario, [FromBody] ModificarExamenDTO modificarExamenDTO)
        {
            ExamenDTO examenDTO = await _examenLogic.ActualizacionExamen(
                idMateria,
                idDiaHorario,
                modificarExamenDTO.IDNuevoDiaHorario);

            if (examenDTO == null)
            {
                return NotFound();
            }

            return Ok(examenDTO);
        }

        [HttpDelete("id-materia/{id-materia}/id-diahorario/{id-diahorario}")]
        public async Task<IActionResult> EliminarPorID(int idMateria, int idDiaHorario)
        {
            try
            {
                await _examenLogic.BajaExamen(idMateria, idDiaHorario);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}