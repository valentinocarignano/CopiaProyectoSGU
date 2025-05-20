using Entidades.DTOs.Respuestas;

namespace Logica.Contracts
{
    public interface IMateriaLogic
    {
        Task AltaMateria(string nombre, List<int> listaProfesores, List<int> listaHorarios, string modalidad, string anio);
        Task BajaMateria(string nombre);
        Task<MateriaDTO> ActualizacionMateria(int id, string nombre, List<int> listaProfesores, List<int> listaHorarios, string modalidad, string anio);
        Task<List<MateriaDTO>> ObtenerMaterias();
        Task<MateriaDTO> ObtenerMateriaNombre(string nombre);
    }
}