using Logica.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaHorarioController : ControllerBase
    {
        private readonly IDiaHorarioLogic _diaHorarioLogic;
        public DiaHorarioController(IDiaHorarioLogic diaHorarioLogic)
        {
            _diaHorarioLogic = diaHorarioLogic;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diaHorarios = await _diaHorarioLogic.ObtenerDiasHorarios();
            return Ok(diaHorarios);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var diaHorario = await _diaHorarioLogic.ObtenerDiaHorarioID(id);
            if (diaHorario == null)
            {
                return NotFound();
            }
            return Ok(diaHorario);
        }
    }
}
