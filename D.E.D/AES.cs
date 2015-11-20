using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

using System.Windows.Forms;

namespace D.E.D
{
    public class AES
    {
        protected static byte[] decoded(byte[] bytesToBeDecrypted, byte[] passwordBytes)
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
                    AES.Padding = PaddingMode.Zeros;
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
                ms.Close();
            }
            return decryptedBytes;
        }
        protected static byte[] encoded(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 2, 15, 240, 232, 39, 204, 190, 33, 226, 206, 110, 107, 61, 25, 87, 196 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Padding = PaddingMode.Zeros;
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

        public void EncryptFile(string file, byte[] passwordBytes, string destination, ProgressBar pb)
        {
            DateTime dt = new DateTime();
            var before = DateTime.Now;
            FileInfo fd = new FileInfo(file);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            int percentage = 0;
            using (Stream source = File.OpenRead(file))
            using (Stream dest = File.Create(destination))
            {
                byte[] buffer = new byte[8192];
                int bytesRead = 0;
                int bytes;
                while ((bytes = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(encoded(buffer, passwordBytes), 0, bytes);
                    bytesRead += bytes;
                    percentage = Convert.ToInt32((double)bytesRead / (double)fd.Length * (double)100);

                    // runs on UI thread
                    pb.Invoke((MethodInvoker)delegate
                    {
                        pb.Value = percentage;
                    });
                }
            }
            LogData(file, before, true);
        }

        public void DecryptFile(string file, byte[] passwordBytes, string destination, ProgressBar pb)
        {
            DateTime dt = new DateTime();
            var before = DateTime.Now;
            FileInfo fd = new FileInfo(file);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            int percentage = 0;
            using (Stream source = File.OpenRead(file))
            using (Stream dest = File.Create(destination))
            {
                byte[] buffer = new byte[8192];
                int bytesRead = 0;
                int bytes;
                while ((bytes = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(decoded(buffer, passwordBytes), 0, bytes);
                    bytesRead += bytes;
                    percentage = Convert.ToInt32((double)bytesRead / (double)fd.Length * (double)100);

                    // runs on UI thread
                    pb.Invoke((MethodInvoker)delegate
                    {
                        pb.Value = percentage;
                    });
                }
            }
            LogData(file, before, false);
        }

        private void LogData(string file, DateTime before, bool encrypt) 
        {
            FileInfo fi = new FileInfo(file);
            TimeSpan span = DateTime.Now - before;
            using (var cn2 = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename" +
            "=|DataDirectory|\\DB.mdf; Integrated Security=True"))
            {
                string sql = @"INSERT INTO [dbo].[Log] ([DateOfAction], [Name], [SizeOfFile], [TimeElapsed], [Encrypted]) VALUES (@DOA, @Name, @SOF, @TE, @Encrypted)";
                var insertCmd2 = new SqlCommand(sql, cn2);
                insertCmd2.Parameters
                    .Add(new SqlParameter("@DOA", SqlDbType.DateTime))
                    .Value = before;
                insertCmd2.Parameters
                    .Add(new SqlParameter("@Name", SqlDbType.VarChar))
                    .Value = file;
                insertCmd2.Parameters
                    .Add(new SqlParameter("@SOF", SqlDbType.VarChar))
                    .Value = fi.Length;
                insertCmd2.Parameters
                    .Add(new SqlParameter("@TE", SqlDbType.Int))
                    .Value = span.TotalSeconds;
                insertCmd2.Parameters
                    .Add(new SqlParameter("@Encrypted", SqlDbType.Bit))
                    .Value = encrypt;
                cn2.Open();
                insertCmd2.ExecuteNonQuery();
                cn2.Close();
            }
        }
    }
}
