using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SqlHelper;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class ScreensAdd : Form
    {
        private Cinema cinema;
        public ScreensAdd(Cinema cinema)
        {
            InitializeComponent();
            this.cinema=cinema;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK && (openfile.FileName != ""))
            {
                pictureBox1.ImageLocation = openfile.FileName;
                openfile.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && pictureBox1.ImageLocation != null)
            {
                string sname = textBox3.Text;
                string row = textBox1.Text;
                string col = textBox2.Text;

                string simage = pictureBox1.ImageLocation;
                string image_Path = @"resource\cinema\S" + sname + ".PNG";

                if (File.Exists(image_Path))
                {
                    File.Delete(image_Path);
                    File.Copy(simage, image_Path);
                }
                else
                {
                    File.Copy(simage, image_Path);
                }

                string insert = "insert into Screens(SName,Row,col,SImage) values('" + sname + "'," + row + "," + col + ",'" + image_Path + "')";
                DbHelperSQL.ExecuteSql(insert);

                

                cinema.Combox_Load();
                cinema.button4_Click(null, null);
                MessageBox.Show("添加成功");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("请输入完整信息！");
            }
        }
    }
}
