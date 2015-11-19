using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Helpers
{
    public class AES
    {
        public static byte[] decoded(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 2, 15, 240, 232, 39, 204, 190, 33, 226, 206, 110, 107, 61, 25, 87, 196 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
            return decryptedBytes;
        }
        public static byte[] encoded(byte[] bytesToBeEncrypted, byte[] passwordBytes) 
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 2, 15, 240, 232, 39, 204, 190, 33, 226, 206, 110, 107, 61, 25, 87, 196 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public static void EncryptFile(string file, byte[] passwordBytes, string destination)
        {
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            using (Stream source = File.OpenRead(file))
            using (Stream dest = File.Create(destination))
            {
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(encoded(buffer, passwordBytes), 0, bytesRead);
                    
                }
            }
        }

        public static void DecryptFile(string file, byte[] passwordBytes, string destination)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(file);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = decoded(bytesToBeDecrypted, passwordBytes);
            File.WriteAllBytes(destination, bytesDecrypted);
        }
    }
}
