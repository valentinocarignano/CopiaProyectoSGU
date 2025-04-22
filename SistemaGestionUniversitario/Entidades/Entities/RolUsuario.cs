namespace Entidades.Entities
{
    public class RolUsuario : EntityBase
    {
        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
        public override string ToString()
        {
            return $"{Descripcion}"; 
        }
    }
}