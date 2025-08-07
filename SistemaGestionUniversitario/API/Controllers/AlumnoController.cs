using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoLogic _alumnoLogic;
        
        public AlumnoController(IAlumnoLogic alumnoLogic)
        {
            _alumnoLogic = alumnoLogic;
        }
        
        // GET: api/Alumnos
        [HttpGet]
        public async Task<IActionResult> GetAlumnos()
        {
            List<AlumnoDTO> alumnos = await _alumnoLogic.ObtenerAlumnos();

            if(alumnos.Count == 0)
            {
                return NoContent();
            }

            return Ok(alumnos);
        }

        [HttpGet("{dni}")]
        public async Task<IActionResult> GetAlumnoPorDNI(string dni)
        {
            try
            {
                AlumnoDTO alumno = await _alumnoLogic.ObtenerAlumnoDNI(dni);

                if (alumno == null)
                {
                    return NotFound();
                }

                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}