namespace Entidades.Entities
{
    public class Profesor : EntityBase
    {
        public DateTime FechaInicioContrato { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Materia>? Materias { get; set; }
        public override string ToString()
        {
            return $"{Usuario.Nombre} {Usuario.Apellido}";
        }
    }
}