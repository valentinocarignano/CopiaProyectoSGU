using Front.Models.Crear;
using Front.Models.Respuestas;
using Front.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Front.Controllers
{
    public class SesionController : Controller
    {
        private readonly IAuthenticator _authenticator;
        public SesionController(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View(new CrearUsuarioLogInFront());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(CrearUsuarioLogInFront usuarioLogIn)
        {
            if (!ModelState.IsValid) return View(usuarioLogIn);

            UsuarioLogInFront? usuarioLogueado = await _authenticator.LogIn(usuarioLogIn.Usuario, usuarioLogIn.Password);
            if (usuarioLogueado == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(usuarioLogIn);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuarioLogueado.Usuario ?? ""),
                new Claim(ClaimTypes.Role, usuarioLogueado.Rol ?? "")
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn");
        }

        public IActionResult AccesoDenegado()
        {
            return View("LogIn");
        }
    }
}