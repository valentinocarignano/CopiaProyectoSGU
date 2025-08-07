using Logica.Contracts;
using Entidades.DTOs.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        private readonly IRolUsuarioLogic _rolUsuarioLogic;
        public RolUsuarioController(IRolUsuarioLogic rolUsuarioLogic)
        {
            _rolUsuarioLogic = rolUsuarioLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRoles()
        {
            List<RolUsuarioDTO> rolUsuarioDTO = await _rolUsuarioLogic.ObtenerRoles();

            if (rolUsuarioDTO.Count == 0)
            {
                return NoContent();
            }

            return Ok(rolUsuarioDTO);
        }
    }
}