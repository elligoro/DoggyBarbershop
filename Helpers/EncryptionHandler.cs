using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DoggyBarbershop.Helpers
{
    public static class EncryptionHandler
    {
        private static IConfiguration _config;
        private static CryptoKeysModel _cryptoKeys;
        public static void Initialize(IConfiguration Configuration)
        {
            _config = Configuration;
            var key = _config.GetValue<string>("SymmetricEncript:Key");
            var iv = _config.GetValue<string>("SymmetricEncript:IVBase64");

            _cryptoKeys = new CryptoKeysModel { Key = key, IVBase64 = iv };
        }

        public static dynamic DecryptAccessToken(string auth)
        {
            if (string.IsNullOrEmpty(auth))
            {
                throw new AccountNotAuthorizedException("auth token is empty");
            }

            auth = auth.Substring("bearer ".Length);
            if (!int.TryParse(EncryptionHandler.Decript(auth), out int accountId))
            {
                throw new AccountNotAuthorizedException("error parsing auth token");
            }

            return accountId;
        }

        public static string Encript(string text) => Encrypt(text, _cryptoKeys.IVBase64, _cryptoKeys.Key);

        public static string Decript(string text) => Decrypt(text, _cryptoKeys.IVBase64, _cryptoKeys.Key);

        private class CryptoKeysModel
        {
            public string Key { get; set; }
            public string IVBase64 { get; set; }
        }

        private static Aes CreateCipher(string keyBase64)
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC

            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = Convert.FromBase64String(keyBase64);

            return cipher;
        }

        private static string Encrypt(string text, string IV, string key)
        {
            Aes cipher = CreateCipher(key);
            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptTransform = cipher.CreateEncryptor();
            byte[] plaintext = Encoding.UTF8.GetBytes(text);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

            return Convert.ToBase64String(cipherText);
        }

        private static string Decrypt(string encryptedText, string IV, string key)
        {
            Aes cipher = CreateCipher(key);
            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptTransform = cipher.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = cryptTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
