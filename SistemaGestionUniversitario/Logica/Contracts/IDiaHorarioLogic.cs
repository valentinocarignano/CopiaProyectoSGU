using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IDiaHorarioLogic
    {
        Task<List<DiaHorario>> ObtenerDiaHorario();
        Task<DiaHorario> ObtenerDiaHorarioID(int id);
    }
}
