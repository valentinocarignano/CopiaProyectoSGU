namespace Entidades.Entities
{
    public class NotaAlumno : EntityBase

    {
        public int IdAlumno {  get; set; }
        public int IdExamen { get; set; }

        public int Nota { get; set; }
    }
}