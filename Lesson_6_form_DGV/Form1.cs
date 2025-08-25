using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lesson_6_form_DGV
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string str = "";
        SqlCommandBuilder cmd = null;
        public Form1()
        {
            InitializeComponent();
            str = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            conn = new SqlConnection(str);
        }

        private void bt_Fill_Click(object sender, EventArgs e)
        {
            try
            {
                ds = new DataSet();
                string sqlq = textBox1.Text;
                da = new SqlDataAdapter(sqlq, conn);
                dataGridView1.DataSource = null;
                cmd = new SqlCommandBuilder(da);
                da.Fill(ds, "Table_1");
                dataGridView1.DataSource = ds.Tables["Table_1"];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void bt_Update_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update(ds, "Table_1");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
