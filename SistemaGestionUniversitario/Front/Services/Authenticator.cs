using Front.Models.Respuestas;

namespace Front.Services
{
    public class Authenticator : IAuthenticator
	{
		private readonly IHttpClientFactory _factory;
		public Authenticator(IHttpClientFactory factory) => _factory = factory;

		public async Task<UsuarioLogInFront?> LogIn(string usuario, string password)
		{
			var client = _factory.CreateClient("ApiPrincipal");

			var payload = new { Usuario = usuario, Password = password };
			var resp = await client.PostAsJsonAsync("Usuario/LogIn", payload);

			if (!resp.IsSuccessStatusCode) return null;

            try
            {
                return await resp.Content.ReadFromJsonAsync<UsuarioLogInFront>();
            }
            catch
            {
                return null;
            }
        }
	}
}