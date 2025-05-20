using Entidades.DTOs.Respuestas;

namespace Logica.Contracts
{
    public interface IHorarioLogic
    {
        Task<List<HorarioDTO>> ObtenerHorarios();
        Task<HorarioDTO> ObtenerHorarioId(int id);
    }
}