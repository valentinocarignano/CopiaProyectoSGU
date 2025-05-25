namespace Entidades.Entities
{
    public class Materia : EntityBase
    {
        public string Nombre { get; set; }
        public int Anio { get; set; }
        public string Modalidad { get; set; }
        public ICollection<Profesor> Profesores { get; set; }
        public ICollection<DiaHorario> DiaHorario { get; set; }
        public ICollection<Examen> Examenes { get; set;}
        public ICollection<Alumno> Alumnos { get; set;}
        public override string ToString()
        {
            return $"{Nombre}";
        }
    }
}