using Entidades.DTOs.Respuestas;
using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IDiaHorarioLogic
    {
        Task<List<DiaHorarioDTO>> ObtenerDiasHorarios();
        Task<DiaHorarioDTO> ObtenerDiaHorarioID(int id);
        Task<DiaHorario?> ObtenerDiaHorarioPorDescripcionUsoInterno(string descripcionDiaHorario);
        Task<String> ObtenerDescripcionDiaHorarioPorIDsUsoInterno(int idDia, int idHorario);
        Task<String> ObtenerDescripcionDiaHorarioPorDiaHorario(DiaHorario diaHorario);
    }
}