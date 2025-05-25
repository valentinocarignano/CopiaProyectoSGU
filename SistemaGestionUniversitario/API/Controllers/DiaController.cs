using Entidades.DTOs.Respuestas;
using Logica.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaController : ControllerBase
    {
        private readonly IDiaLogic _diaLogic;
        public DiaController(IDiaLogic diaLogic)
        {
            _diaLogic = diaLogic;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDias()
        {
            List<DiaDTO> diaDTO = await _diaLogic.ObtenerDias();

            return Ok(diaDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerDiaPorID(int id)
        {
            DiaDTO diaDTO = await _diaLogic.ObtenerDiaId(id);

            if (diaDTO == null)
            {
                return NotFound();
            }

            return Ok(diaDTO);
        }
    }
}