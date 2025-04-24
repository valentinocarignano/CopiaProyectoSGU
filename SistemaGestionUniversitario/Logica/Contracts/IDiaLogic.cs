using Entidades.DTOs;

namespace Logica.Contracts
{
    public interface IDiaLogic
    {
        Task<List<DiaDTO>> ObtenerDias();
        Task<DiaDTO> ObtenerDiaId(int id);
    }
}
