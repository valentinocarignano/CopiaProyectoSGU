using Entidades.DTOs.Respuestas;
using Logica.Contracts;
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

            return Ok(profesorDTO);
        }
        [HttpGet]
        [Route("{dni}")]
        public async Task<IActionResult> ObtenerProfesor(string dni)
        {
            ProfesorDTO profesorDTO = await _profesorLogic.ObtenerProfesorDNI(dni);

            return Ok(profesorDTO);
        }
    }
}