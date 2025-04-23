namespace Logica
{
    public class PasswordHelper
    {
        // Método para hashear la contraseña
        public static string HashPassword(string password)
        {
            // Genera el hash de la contraseña con un salt automáticamente generado
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método para verificar la contraseña con el hash almacenado
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verifica si la contraseña dada coincide con el hash almacenado
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}