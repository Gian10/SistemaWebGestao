using Microsoft.CodeAnalysis.Scripting;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Cryptography;
using System.Text;


namespace Sistema_Web_Gestao.Helper
{

    // Uso da classe para Criptofrafia de Senha
    public static class Useful
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("ChaveSecreta1234"); // 16 bytes (128 bits)
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("Inicializa123456");  // 16 bytes (128 bits)

        public static string Encrypt(string senha)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(senha);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
