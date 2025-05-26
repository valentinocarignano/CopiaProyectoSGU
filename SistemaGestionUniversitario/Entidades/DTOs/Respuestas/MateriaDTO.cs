namespace Entidades.DTOs.Respuestas
{
    public class MateriaDTO
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public string Modalidad { get; set; }
        public List<string> NombresProfesores { get; set; }
        public List<string> DescripcionDiasHorarios { get; set; }
    }
}