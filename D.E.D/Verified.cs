using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace D.E.D
{
    public partial class Verified : Form
    {
        public Verified(bool? isVerified)
        {
            if (isVerified.HasValue == null && isVerified == true)
            {
                Main frm = new Main();
                frm.Show();
                Close();
            }
            InitializeComponent();
        }

        private void Verified_Load(object sender, EventArgs e)
        {
            this.logTableAdapter1.Fill(this.dBDataSet1.Log);
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult openResult = openFileDialog1.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                DialogResult destination = folderBrowserDialog1.ShowDialog();
                if (destination == DialogResult.OK)
                {
                    var bytes = File.ReadAllBytes(openFileDialog1.FileName);

                    using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename" +
                 "=|DataDirectory|\\DB.mdf; Integrated Security=True"))
                    {
                        string _sql = @"SELECT * FROM [dbo].[Data]";
                        var cmd = new SqlCommand(_sql, cn);
                        cn.Open();
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    FileInfo fi = new FileInfo(openFileDialog1.FileName);
                                    DateTime dt = new DateTime();
                                    var before = DateTime.Now;
                                    AES aes = new AES();
                                    Task.Factory.StartNew(() => aes.EncryptFile(openFileDialog1.FileName, Convert.FromBase64String(reader["70617373776f7264"].ToString()), folderBrowserDialog1.SelectedPath + "\\" + openFileDialog1.SafeFileName, pbEncrypt));
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
                                            .Value = openFileDialog1.FileName;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@SOF", SqlDbType.VarChar))
                                            .Value = fi.Length;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@TE", SqlDbType.Int))
                                            .Value = span.TotalSeconds;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@Encrypted", SqlDbType.Bit))
                                            .Value = true;
                                        cn2.Open();
                                        insertCmd2.ExecuteNonQuery();
                                        cn2.Close();
                                    }
                                    this.logTableAdapter1.Fill(this.dBDataSet1.Log);
                                }
                            }
                            catch (Exception e2) { MessageBox.Show(e2.Message); }
                        }
                        reader.Dispose();
                        cmd.Dispose();
                    }               
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult openResult = openFileDialog1.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                DialogResult destination = folderBrowserDialog1.ShowDialog();
                if (destination == DialogResult.OK)
                {
                    var bytes = File.ReadAllBytes(openFileDialog1.FileName);

                    using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename" +
                 "=|DataDirectory|\\DB.mdf; Integrated Security=True"))
                    {
                        string _sql = @"SELECT * FROM [dbo].[Data]";
                        var cmd = new SqlCommand(_sql, cn);
                        cn.Open();
                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    FileInfo fi = new FileInfo(openFileDialog1.FileName);
                                    DateTime dt = new DateTime();
                                    var before = DateTime.Now;
                                    AES aes = new AES();
                                    Task.Factory.StartNew(() => aes.DecryptFile(openFileDialog1.FileName, Convert.FromBase64String(reader["70617373776f7264"].ToString()), folderBrowserDialog1.SelectedPath + "\\" + openFileDialog1.SafeFileName, pbDecrypt));
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
                                            .Value = openFileDialog1.FileName;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@SOF", SqlDbType.VarChar))
                                            .Value = fi.Length;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@TE", SqlDbType.Int))
                                            .Value = span.TotalSeconds;
                                        insertCmd2.Parameters
                                            .Add(new SqlParameter("@Encrypted", SqlDbType.Bit))
                                            .Value = false;
                                        cn2.Open();
                                        insertCmd2.ExecuteNonQuery();
                                        cn2.Close();
                                    }
                                    this.logTableAdapter1.Fill(this.dBDataSet1.Log);
                                }
                            }
                            catch (Exception e2) { MessageBox.Show(e2.Message); }
                        }
                        reader.Dispose();
                        cmd.Dispose();
                    }
                }
            }
        }

    }
}
