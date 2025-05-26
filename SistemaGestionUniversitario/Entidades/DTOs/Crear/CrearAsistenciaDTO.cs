namespace Entidades.DTOs.Crear
{
    public class CrearAsistenciaDTO
    {
        public int idInscripcion { get; set; }
        public int idDiaHorarioMateria { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha { get; set; }
    }
}