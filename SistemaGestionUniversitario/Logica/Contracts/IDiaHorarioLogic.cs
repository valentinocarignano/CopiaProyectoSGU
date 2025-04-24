using Entidades.DTOs;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IDiaHorarioLogic
    {
        Task<List<DiaHorarioDTO>> ObtenerDiasHorarios();
        Task<DiaHorarioDTO> ObtenerDiaHorarioID(int id);
    }
}
