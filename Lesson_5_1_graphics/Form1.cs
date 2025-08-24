using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Lesson_5_1_graphics
{
    public partial class Form1 : Form
    {
        SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        string str = "";
        string filename = "";
        public Form1()
        {
            InitializeComponent();
            str = ConfigurationManager.ConnectionStrings["Company_db"].ConnectionString;
            conn = new SqlConnection(str);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Графические файлы | *.bmp; *.jpg; *.png; *.jpeg; *.gif";
            ofd.FileName = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filename = ofd.FileName;
                LoadPicture();
            }
        }

        private void LoadPicture()
        {
            try
            {
                byte[] bytes = CreateCopy();
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "insert into dbo.Pictures(Customer_ID,_Name,Picture)" +
                    "values (@customerID, @name, @picture);", conn);

                if (textBox1.Text == null || textBox1.Text.Length == 0)
                {
                    return;
                }
                int index = -1;
                int.TryParse(textBox1.Text, out index);
                cmd.Parameters.Add("@customerID", SqlDbType.Int).Value = index;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 255).Value = filename;
                cmd.Parameters.Add("@picture", SqlDbType.Image, bytes.Length).Value = bytes;
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show("Error LoadPicture");
            }
            finally 
            {
                if (conn != null) 
                {
                    conn.Close();
                }
            }
        }

        private byte[] CreateCopy()
        {
            try
            {
                Image img = Image.FromFile(filename);
                int maxWidth = 300, maxHeight = 300;
                double ratioX = (double)maxWidth / img.Width;
                double ratioY = (double)maxHeight / img.Height;
                double ratio = Math.Min(ratioX, ratioY);
                int newWidth = (int)(img.Width * ratio);
                int newHeight = (int)(img.Height * ratio);

                Image im = new Bitmap(newWidth, newHeight);
                Graphics g = Graphics.FromImage(im);
                g.DrawImage(im, 0, 0, newWidth, newHeight);
                MemoryStream ms = new MemoryStream();

                im.Save(ms, ImageFormat.Jpeg);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(ms);
                byte[] buf = br.ReadBytes((int)ms.Length);
                return buf;
            }
            catch (Exception)
            {
                MessageBox.Show("Error CreateCopy");
                return null;
            }
        }

        private void ShowOne()
        {
            try
            {
                if (textBox1.Text == null || textBox1.Text.Length == 0)
                {
                    MessageBox.Show("Укажите id клиента");
                    return;
                }
                int index = -1;
                int.TryParse(textBox1.Text, out index);
                if (index == -1)
                {
                    MessageBox.Show("Укажите id клиента  в правильном формате");
                    return;
                }
                da = new SqlDataAdapter("select Picture from dbo.Pictures where Customer_ID=@Id", conn);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = index;
                ds = new DataSet();
                da.Fill(ds);
                byte[] bytes = (byte[])ds.Tables[0].Rows[0]["Picture"];
                MemoryStream ms = new MemoryStream(bytes);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowAll()
        {
            try
            {
                da = new SqlDataAdapter("select * from dbo.Pictures;", conn);
                SqlCommandBuilder cmd = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "picture");
                dataGridView1.DataSource = ds.Tables["Picture"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowOne();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowAll();
        }
    }
}