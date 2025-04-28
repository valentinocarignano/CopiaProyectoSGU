using Datos.Repositories.Contracts;
using Entidades.DTOs;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class DiaHorarioLogic : IDiaHorarioLogic
    {
        private IDiaHorarioRepository _diaHorarioRepository;

        public DiaHorarioLogic(IDiaHorarioRepository diaHorarioRepository)
        {
            _diaHorarioRepository = diaHorarioRepository;;
        }

        public async Task<List<DiaHorarioDTO>> ObtenerDiasHorarios()
        {
            try
            {
                List<DiaHorario> listaDiasHorarios = (await _diaHorarioRepository.FindAllAsync()).ToList();

                if (listaDiasHorarios == null)
                {
                    return null;
                }

                List<DiaHorarioDTO> listaDiasHorariosDTO = listaDiasHorarios.Select(t => new DiaHorarioDTO
                {
                    ID = t.ID,
                    DescripcionDia = t.GetDescripcionDia(),
                    DescripcionHorario = t.GetDescripcionHorario()
                }).ToList();

                return listaDiasHorariosDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            };
        }
        public async Task<DiaHorarioDTO> ObtenerDiaHorarioID(int id)
        {
            DiaHorario? diaHorario = (await _diaHorarioRepository.FindByConditionAsync(t => t.ID == id)).FirstOrDefault();

            if (diaHorario == null)
            {
                return null;
            }

            DiaHorarioDTO diaHorarioDTO = new DiaHorarioDTO()
            {
                ID = diaHorario.ID,
                DescripcionDia = diaHorario.GetDescripcionDia(),
                DescripcionHorario = diaHorario.GetDescripcionHorario()
            };

            return diaHorarioDTO;
        }
    }
}