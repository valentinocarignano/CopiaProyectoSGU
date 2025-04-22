namespace Entidades.Entities
{
    public class Usuario : EntityBase
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }

        public string CaracteristicaTelefono { get; set; }
        public string NumeroTelefono { get; set; }

        public string Localidad { get; set; }
        public string Direccion { get; set; }

        public RolUsuario RolUsuario { get; set; }
        public ICollection<Profesor> Profesores { get; set; }
        public ICollection<Alumno> Alumnos { get; set; }
        
        public override string ToString()
        {
            return $"{DNI}";
        }
    }
}