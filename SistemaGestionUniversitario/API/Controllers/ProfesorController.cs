using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Logica.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorLogic _profesorLogic;
        public ProfesorController(IProfesorLogic profesorLogic)
        {
            _profesorLogic = profesorLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProfesores()
        {
            List<ProfesorDTO> profesorDTO = await _profesorLogic.ObtenerProfesores();

            if (profesorDTO.Count == 0)
            {
                return NoContent();
            }

            return Ok(profesorDTO);
        }

        [HttpGet]
        [Route("{dni}")]
        public async Task<IActionResult> ObtenerProfesor(string dni)
        {
            try
            {
                ProfesorDTO profesorDTO = await _profesorLogic.ObtenerProfesorDNI(dni);

                if (profesorDTO == null)
                {
                    return NotFound();
                }

                return Ok(profesorDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            } 
        }
    }
}