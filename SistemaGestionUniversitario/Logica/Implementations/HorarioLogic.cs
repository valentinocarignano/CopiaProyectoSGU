using Datos.Repositories.Contracts;
using Entidades.DTOs.Respuestas;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class HorarioLogic :  IHorarioLogic
    {
        private IHorarioRepository _horarioRepository;

        public HorarioLogic(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public async Task<List<HorarioDTO>> ObtenerHorarios()
        {
            try
            {
                List<Horario> listaHorarios = (await _horarioRepository.FindAllAsync()).ToList();

                if (listaHorarios == null)
                {
                    return null;
                }

                List<HorarioDTO> listaHorariosDTO = listaHorarios.Select(t => new HorarioDTO
                {
                    ID = t.ID,
                    Descripcion = t.Descripcion
                }).ToList();

                return listaHorariosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<HorarioDTO> ObtenerHorarioId(int id)
        {
            Horario? horario = (await _horarioRepository.FindByConditionAsync(t => t.ID == id)).FirstOrDefault();

            if (horario == null)
            {
                return null;
            }

            HorarioDTO horarioDTO = new HorarioDTO()
            {
                ID = horario.ID,
                Descripcion = horario.Descripcion
            };

            return horarioDTO;
        }
    }
}