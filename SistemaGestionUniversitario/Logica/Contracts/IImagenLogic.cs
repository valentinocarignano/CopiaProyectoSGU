using Entidades.Entities;

namespace Logica.Contracts
{
    public interface IImagenLogic
    {
        void GuardarImagenEnBaseDeDatos(string rutaArchivo, string formularioOrigen);
        Task<List<Imagen>> ObtenerImagenesPorOrigen(string formularioOrigen);
    }
}
