using Entidades.DTOs.Crear;
using Entidades.DTOs.Modificar;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaLogic _materiaLogic;

        public MateriaController(IMateriaLogic materiaLogic)
        {
            _materiaLogic = materiaLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterias()
        {
            List<MateriaDTO> materiasDTO = await _materiaLogic.ObtenerMaterias();

            return Ok(materiasDTO);
        }

        [HttpGet("{nombreMateria}")]
        public async Task<IActionResult> GetMaterias(string nombreMateria)
        {
            MateriaDTO materiaDTO = await _materiaLogic.ObtenerMateriaNombre(nombreMateria);

            return Ok(materiaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostMateria([FromBody] CrearMateriaDTO materia)
        {
            await _materiaLogic.AltaMateria(
                materia.Nombre,
                materia.ProfesoresIDs,
                materia.DiasHorariosIDs,
                materia.Modalidad,
                materia.Anio);

            return Ok("Materia creada correctamente.");
        }

        [HttpPut("{nombreMateria}")]
        public async Task<IActionResult> PutMateria(string nombreMateria, [FromBody] ModificarMateriaDTO materia)
        {
            MateriaDTO materiaDTO = await _materiaLogic.ActualizacionMateria(
                nombreMateria,
                materia.ProfesoresIDs,
                materia.DiasHorariosIDs);

            if (materiaDTO == null)
            {
                return NotFound();
            }

            return Ok(materiaDTO);
        }

        [HttpDelete("{nombreMateria}")]
        public async Task<IActionResult> DeleteMateria(string nombreMateria)
        {
            try
            {
                await _materiaLogic.BajaMateria(nombreMateria);

                return Ok($"La materia {nombreMateria} fue eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}