using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IHorarioLogic
    {
        Task<List<Horario>> ObtenerHorarios();
        Task<Horario> ObtenerHorarioPorId(int IdHorario);

    }
}
