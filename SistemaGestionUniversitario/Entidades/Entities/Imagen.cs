namespace Entidades.Entities
{
    public class Imagen : EntityBase
    {
        public string Nombre { get; set; }
        public string URL { get; set; }
        public long Tamanio { get; set; }
        public string TipoMime { get; set; }
        public string FormularioOrigen {  get; set; }
    }
}