using HeyRed.Mime;
using Negocio.Contracts;
using Negocio.Repositories;
using Shared.Dtos.Usuario;
using Shared.Entities;
using Shared.Repositories;
using Shared.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logica.Implementations
{
    public class ImagenLogic: IImagenLogic
    {
        private IImagenRepository _imagenRepository;

        public ImagenLogic(IImagenRepository imagenRepository)
        {
            _imagenRepository = imagenRepository;
        }

        public void GuardarImagenEnBaseDeDatos(string rutaArchivo, string formulario_usuarioOrigen)
        {
            // Crear una instancia de la clase Imagen
            Imagen nuevaImagen = new Imagen
            {
                Nombre = Path.GetFileName(rutaArchivo),     // Nombre del archivo
                URL = rutaArchivo,                                // Ruta completa del archivo
                Tamanio = new FileInfo(rutaArchivo).Length,        // Tamaño del archivo en bytes
                TipoMime = MimeTypesMap.GetMimeType(rutaArchivo), // Obtener el tipo MIME con MimeTypesMap
                FormularioOrigen = formulario_usuarioOrigen // Formulario o Usuario al que pertenece el archivo
            };

            Imagen? imagenExistente = _imagenRepository.FindByCondition(img => img.FormularioOrigen == formulario_usuarioOrigen).FirstOrDefault();

            if (imagenExistente == null)
            {
                _imagenRepository.Create(nuevaImagen);
            }
            else
            {
                imagenExistente.Nombre = nuevaImagen.Nombre;
                imagenExistente.URL = nuevaImagen.URL;
                imagenExistente.Tamanio = nuevaImagen.Tamanio;
                imagenExistente.TipoMime = nuevaImagen.TipoMime;

                _imagenRepository.Update(imagenExistente);
            }

            _imagenRepository.Save();
        }

        public async Task<List<Imagen>> ObtenerImagenesPorOrigen(string formularioOrigen)
        {
            return await ImagenRepos.GetByFormName(formularioOrigen);
        }

    }
}
