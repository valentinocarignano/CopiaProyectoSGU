using System.Text.RegularExpressions;

namespace Logica
{
    public class ValidacionesCampos
    {
        public static bool DocumentoEsValido(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento) || documento.Length < 8)
            {
                return false;
            }

            foreach (char c in documento)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool TextoEsValido(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return false;
            }

            texto = texto.Trim();

            if (texto.Contains("  "))
            {
                return false;
            }

            if (texto.Length < 3 || texto.Length > 30)
            {
                return false;
            }

            foreach (char c in texto)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool NumeroTelefonoEsValido(string caracteristica, string numero)
        {
            if (string.IsNullOrEmpty(caracteristica) || string.IsNullOrEmpty(numero))
            {
                return false;
            }

            var regex_caracteristica = new Regex(@"^\+\d{1,4}\s?\d{2,5}$");
            var regex_numero = new Regex(@"^\d{6,}$");

            return regex_caracteristica.IsMatch(caracteristica) && regex_numero.IsMatch(numero);
        }
        public static bool ModalidadMateriaEsValida(string texto)
        {
            if (texto != "Práctica" && texto != "Teórica" && texto != "Teórica/Práctica")
            {
                return false;
            }

            return true;
        }
        public static bool TipoExamenEsValido(string texto)
        {
            if (texto != "Parcial" && texto != "Final")
            {
                return false;
            }

            return true;
        }
    }
}