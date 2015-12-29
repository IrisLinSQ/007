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
using ClassUse;

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
                int sname = Convert.ToInt16(textBox3.Text);
                int row = Convert.ToInt16(textBox1.Text);
                int col = Convert.ToInt16(textBox2.Text);

                string simage = pictureBox1.ImageLocation;
                string image_Path = @"resource\cinema\S" + textBox3.Text.Trim() + ".PNG";

                if (File.Exists(image_Path))
                {
                    File.Delete(image_Path);
                    File.Copy(simage, image_Path);
                }
                else
                {
                    File.Copy(simage, image_Path);
                }

                Screens screen = new Screens(sname, row, col, image_Path);
                SqlScreens.scressAdd(screen);

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
