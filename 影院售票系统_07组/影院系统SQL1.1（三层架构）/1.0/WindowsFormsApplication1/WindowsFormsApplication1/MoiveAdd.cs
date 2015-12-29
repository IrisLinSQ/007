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
    public partial class MoiveAdd : Form
    {
        private Cinema cinema;
        public MoiveAdd(Cinema cinema)
        {
            InitializeComponent();
            this.cinema = cinema;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" &&
                textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "" && pictureBox1.ImageLocation != null)
            {

                string mname = textBox1.Text;
                string mtype = textBox4.Text;
                string mdirector = textBox2.Text;
                string mactor = textBox3.Text;
                DateTime mputdat = Convert.ToDateTime(textBox5.Text.Trim());
                int mlength = Convert.ToInt16(textBox7.Text);
                float mfare = (float)Convert.ToDouble(textBox6.Text);
                string filePath = @"resource\introduction\" + mname.Trim() + "简介.txt";

                string mimage = pictureBox1.ImageLocation;

                string imagePath = @"resource\image\" + mname.Trim() + ".png";
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    File.Copy(@mimage, @imagePath);
                }
                else
                {
                    File.Copy(@mimage, @imagePath);
                }
                

                string insert = "insert into Movie(MName,MType,MDirector,MActor,MPutDay,MLength,MFare,MImage,MStory) values('"+mname+"','"+mtype+"','"+mdirector+"','"+mactor+"','"+mputdat+"',"+mlength+","+mfare+",'"+imagePath+"','"+@filePath+"')";
                DbHelperSQL.ExecuteSql(insert);

                FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(textBox8.Text);
                sw.Close();
                fs.Close();

                MessageBox.Show("添加成功");
                cinema.Combox_Load();
                cinema.button3_Click(null, null);
                this.Dispose();
            }
            else
            {
                MessageBox.Show("请输入完整信息！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == DialogResult.OK && (openfile.FileName != ""))
            {
                pictureBox1.ImageLocation = openfile.FileName;
                
            
            openfile.Dispose();
            }
        }
    }
}
