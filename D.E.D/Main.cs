using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace D.E.D
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            var input = txtUnlock.Text;

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
                            if (reader["70617373776f7264"].ToString().Equals(SHA1.Encode(input)))
                            {
                                Close();
                                Verified frm = new Verified(true);
                                frm.Show();
                            }
                            else 
                            {
                                txtUnlock.Text = "";
                                btnUnlock.Enabled = false;
                                AutoClosingMessageBox.Show("Invalid Password", "Error", 5000);
                                btnUnlock.Enabled = true;
                            }
                        }
                    }
                    catch (Exception e2) { return; }

                }
                reader.Dispose();
                cmd.Dispose();
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename" +
            "=|DataDirectory|\\DB.mdf; Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [dbo].[Data]";
                var cmd = new SqlCommand(_sql, cn);
                cn.Open();
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    Close();
                    Create frm = new Create();
                    frm.Show();
                }
                reader.Dispose();
                cmd.Dispose();
            }
        }
        private class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }
    }
}
