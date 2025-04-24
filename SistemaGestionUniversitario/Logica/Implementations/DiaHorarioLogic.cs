using Datos.Repositories.Contracts;
using Entidades.DTOs;
using Entidades.Entities;
using Logica.Contracts;

namespace Logica.Implementations
{
    public class DiaHorarioLogic : IDiaHorarioLogic
    {
        private IDiaHorarioRepository _diaHorarioRepository;
        private IDiaRepository _diaRepository;
        private IHorarioRepository _horarioRepository;

        public DiaHorarioLogic(IDiaHorarioRepository diaHorarioRepository, IDiaRepository diaRepository, IHorarioRepository horarioRepository)
        {
            _diaHorarioRepository = diaHorarioRepository;
            _diaRepository = diaRepository;
            _horarioRepository = horarioRepository;
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
        public async Task<DiaHorarioDTO> ObtenerDiaHorarioId(int id)
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