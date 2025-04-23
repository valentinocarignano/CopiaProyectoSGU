using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IDiaLogic
    {
        Task<List<Dia>> ObtenerDias();
        Task<Dia> ObtenerDiasPorId(int IdDia);
    }
}
