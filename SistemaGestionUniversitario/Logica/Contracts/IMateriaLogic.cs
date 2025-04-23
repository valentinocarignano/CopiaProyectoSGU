using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IMateriaLogic
    {
        Task AltaMateria(string nombre, List<int> listaProfesores, List<int> listaHorarios, string modalidad, string anio);
        Task BajaMateria(string nombre);
        Task ActualizacionMateria(int id, string nombre, List<int> listaProfesores, List<int> listaHorarios, string modalidad, string anio);
        Task<List<Materia>> ObtenerMaterias();
        //Task<List<MateriaListadoDTO>> ObtenerMateriasParaListado(string? filtroSeleccionado);
        Task<List<dynamic>> ObtenerMateriasParaGrid();
        List<Materia> ListaMaterias();
        Materia ObtenerMateriaPorNombre(string nombre);

    }
}
