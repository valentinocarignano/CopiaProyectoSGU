using Front.Models.Sesion;

namespace Front.Services
{
	public class Authenticator : IAuthenticator
	{
		private readonly IHttpClientFactory _factory;
		public Authenticator(IHttpClientFactory factory) => _factory = factory;

		public async Task<UsuarioLogInFront?> LogInAsync(string usuario, string password)
		{
			var client = _factory.CreateClient("ApiPrincipal");

			var payload = new { Usuario = usuario, Password = password };
			var resp = await client.PostAsJsonAsync("Authenticator/Login", payload); // ajusta la ruta de tu API

			if (!resp.IsSuccessStatusCode) return null;

			return await resp.Content.ReadFromJsonAsync<UsuarioLogInFront>();
		}
	}
}
