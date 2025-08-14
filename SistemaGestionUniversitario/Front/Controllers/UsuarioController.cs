using Front.Models.Crear;
using Front.Models.Modificar;
using Front.Models.Respuestas;
using Microsoft.AspNetCore.Mvc;

namespace Front.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IHttpClientFactory httpClientFactory, ILogger<UsuarioController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ApiPrincipal");
            _logger = logger;
        }

        // GET: /Usuario/GetUsuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<UsuarioFront>? usuarios = await _httpClient.GetFromJsonAsync<List<UsuarioFront>>("Usuario");
                return View(usuarios ?? new List<UsuarioFront>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios desde la API");
                // TODO: Pregunta -> Redirigir a vista de error o devolver vacío??
                return View(new List<UsuarioFront>());
            }
        }

        // GET: /Usuario/GetUsuarioDNI/dni
        [HttpGet]
        public async Task<IActionResult> GetUsuarioDNI(string dni)
        {
            try
            {
                UsuarioFront? usuario = await _httpClient.GetFromJsonAsync<UsuarioFront>($"Usuario/{dni}");
                
                if (usuario == null)
                {
                    TempData["Error"] = "Usuario inexistente o no encontrado.";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuario por DNI");
                return RedirectToAction("GetUsuarios");
            }
        }

        // POST: /Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario(CrearUsuarioFront usuario)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Usuario", usuario);
                
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", error);
                    return View(usuario);
                }

                TempData["Success"] = "Usuario creado correctamente.";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                return View(usuario);
            }
        }

        // PUT: /Usuario/dni
        [HttpPost]
        public async Task<IActionResult> UpdateUsuario(string dni, ModificarUsuarioFront usuario)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Usuario/{dni}", usuario);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ModelState.AddModelError("", "No se encontró el usuario con ese DNI.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error de validación: {error}");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrió un error inesperado al actualizar el usuario.");
                    }

                    return View(usuario);
                }

                TempData["Success"] = "Usuario actualizado correctamente.";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario");
                return View(usuario);
            }
        }

        // PUT: /Usuario/actualizarPassword/dni
        [HttpPost]
        public async Task<IActionResult> UpdatePasswordUsuario(string dni, ModificarUsuarioFront usuario)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Usuario/actualizarPassword/{dni}", usuario);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        ModelState.AddModelError("", "No se encontró el usuario con ese DNI.");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"Error de validación: {error}");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ocurrió un error inesperado al actualizar la contraseña.");
                    }

                    return View(usuario);
                }

                TempData["Success"] = "Contraseña actualizada correctamente.";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la contraseña");
                return View(usuario);
            }
        }

        // DELETE: /Usuario/dni
        [HttpPost]
        public async Task<IActionResult> DeleteUsuario(string dni)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"Usuario/{dni}");

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        TempData["Error"] = "No se encontró el usuario con ese DNI.";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        string error = await response.Content.ReadAsStringAsync();
                        TempData["Error"] = $"Error de validación: {error}";
                    }
                    else
                    {
                        TempData["Error"] = "Ocurrió un error inesperado al eliminar el usuario.";
                    }

                    return RedirectToAction("GetUsuarios");
                }

                TempData["Success"] = "Usuario eliminado correctamente.";
                return RedirectToAction("GetUsuarios");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar usuario");
                TempData["Error"] = "Ocurrió un error al eliminar el usuario.";
                return RedirectToAction("GetUsuarios");
            }
        }
    }
}