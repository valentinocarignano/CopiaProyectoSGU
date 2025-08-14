using Front.Models.Respuestas;

namespace Front.Services
{
    public interface IAuthenticator
    {
        Task<UsuarioLogInFront?> LogIn(string usuario, string password);
    }
}