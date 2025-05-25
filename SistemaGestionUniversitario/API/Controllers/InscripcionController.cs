using Entidades.DTOs.Crear;
using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly IInscripcionLogic _inscripcionLogic;
        public InscripcionController(IInscripcionLogic inscripcionLogic)
        {
            _inscripcionLogic = inscripcionLogic;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerInscripciones()
        {
            try
            {
                List<InscripcionDTO> inscripcionDTO = await _inscripcionLogic.ObtenerInscripciones();
                return Ok(inscripcionDTO);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AltaInscripcion([FromBody]CrearInscripcionDTO crearInscripcionDTO)
        {
            try
            {
                await _inscripcionLogic.AltaInscripcion(crearInscripcionDTO.IdAlumno, crearInscripcionDTO.IdMateria);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("{idMateria}/{idAlumno}")]
        public async Task<IActionResult> BajaInscripcion(string idMateria, string idAlumno)
        {
            try
            {
                await _inscripcionLogic.BajaInscripcion(idMateria, idAlumno);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
