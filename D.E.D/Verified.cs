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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        } 
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            FilesCompleted();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult openResult = openFileDialog1.ShowDialog();
            
            if (openResult == DialogResult.OK)
            {
                DialogResult destination = folderBrowserDialog1.ShowDialog();
                if (destination == DialogResult.OK)
                {
                    var bytes = File.ReadAllBytes(openFileDialog1.FileName);
                    byte[] obtainedData = null;
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
                                    obtainedData = Convert.FromBase64String(reader["70617373776f7264"].ToString());
                                }
                            }
                            catch (Exception e2) { MessageBox.Show(e2.Message); }
                        }
                        reader.Dispose();
                        cmd.Dispose();
                    }
                    if (obtainedData != null)
                    {
                        var bytesToAllocate = 10240;
                        if (GC.GetTotalMemory(false) > 150000 && GC.GetTotalMemory(false) < 250000) 
                        {
                            bytesToAllocate = 10240 * 2;
                        }
                        if (GC.GetTotalMemory(false) < 100000)
                        {
                            bytesToAllocate = 10240 / 2;
                        }
                        else
                            bytesToAllocate = 10240 * 3;
                        AES aes = new AES();
                        string[] destinations = new string[openFileDialog1.SafeFileNames.Length];
                        int i = 0;
                        foreach(var _destination in openFileDialog1.SafeFileNames) 
                        {
                            destinations[i] = folderBrowserDialog1.SelectedPath + "\\" + _destination;
                            i++;
                        }
                        Task.Factory.StartNew(() => aes.EncryptFile(openFileDialog1.FileNames
                            , obtainedData
                            , destinations
                            , pbEncrypt
                            , bytesToAllocate)).ContinueWith(t =>
                            {
                                dataGridView1.Invoke((MethodInvoker)delegate
                                {
                                    logTableAdapter1.Fill(dBDataSet1.Log);
                                });
                            });
                    }
                }
            }
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            FilesCompleted();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult openResult = openFileDialog1.ShowDialog();
            if (openResult == DialogResult.OK)
            {
                DialogResult destination = folderBrowserDialog1.ShowDialog();
                if (destination == DialogResult.OK)
                {
                    var bytes = File.ReadAllBytes(openFileDialog1.FileName);
                    byte[] obtainedData = null;
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
                                    obtainedData = Convert.FromBase64String(reader["70617373776f7264"].ToString());
                                }
                            }
                            catch (Exception e2) { MessageBox.Show(e2.Message); }
                        }
                        reader.Dispose();
                        cmd.Dispose();
                    }
                    if (obtainedData != null)
                    {
                        var bytesToAllocate = 10240;
                        if (GC.GetTotalMemory(false) > 150000 && GC.GetTotalMemory(false) < 250000) 
                        {
                            bytesToAllocate = 10240 * 2;
                        }
                        if (GC.GetTotalMemory(false) < 100000)
                        {
                            bytesToAllocate = 10240 / 2;
                        }
                        else
                            bytesToAllocate = 10240 * 3;
                        AES aes = new AES();

                        string[] destinations = new string[openFileDialog1.SafeFileNames.Length];
                        int i = 0;
                        foreach(var _destination in openFileDialog1.SafeFileNames) 
                        {
                            destinations[i] = folderBrowserDialog1.SelectedPath + "\\" + _destination;
                            i++;
                        }
                        Task.Factory.StartNew(() => aes.DecryptFile(openFileDialog1.FileNames
                            , obtainedData
                            , destinations
                            , pbEncrypt
                            , bytesToAllocate)).ContinueWith(t =>
                            {
                                dataGridView1.Invoke((MethodInvoker)delegate
                                {
                                    logTableAdapter1.Fill(dBDataSet1.Log);
                                });
                            });
                    }
                }
            }
        }
        private void FilesCompleted() 
        {
            pbDecrypt.Value = 0;
            pbEncrypt.Value = 0;
        }
    }
}
