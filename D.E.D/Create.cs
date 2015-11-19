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
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            var input = txtCreate.Text;

            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename" +
             "=|DataDirectory|\\DB.mdf; Integrated Security=True"))
            {
                string _sql = @"INSERT INTO [dbo].[Data] ([70617373776f7264]) VALUES (@70617373776f7264)";
                var insertCmd = new SqlCommand(_sql, cn);
                insertCmd.Parameters
                    .Add(new SqlParameter("@70617373776f7264", SqlDbType.VarChar))
                    .Value = SHA1.Encode(input);
                cn.Open();
                insertCmd.ExecuteNonQuery();
                cn.Close();
            }
            Main frm = new Main();
            frm.Show();
            Close();
        }
    }
}
