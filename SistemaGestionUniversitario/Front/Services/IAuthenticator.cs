using Front.Models.Sesion;

namespace Front.Services
{
	public interface IAuthenticator
	{
		Task<UsuarioLogInFront?> LogInAsync(string usuario, string password);
	}
}