namespace Entidades.Entities
{
    public class Examen: EntityBase
    {

        public string Tipo { get; set; }
        public Materia Materia { get; set; }
        public DiaHorario DiaHorario { get; set; }
        public ICollection<Alumno> Alumnos { get; set; }
    }
}