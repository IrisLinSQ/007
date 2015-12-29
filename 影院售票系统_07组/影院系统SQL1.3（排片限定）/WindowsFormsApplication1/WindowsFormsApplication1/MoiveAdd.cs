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
            comboBox1.SelectedItem = null;
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            pictureBox1.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" &&
                comboBox1.SelectedItem != null && textBox5.Text != "" && textBox6.Text != "" &&
                textBox7.Text != "" && textBox8.Text != "" && pictureBox1.ImageLocation != null)
            {
                Moive moive = new Moive();

                string filePath = @"resource\introduction\" + textBox1.Text.Trim() + "简介.txt";
                string mimage = pictureBox1.ImageLocation;//插入的图片的位置
                string imagePath = @"resource\image\" + textBox1.Text.Trim() + ".png";//复制到目标位置下的地址

                moive.setName(textBox1.Text.Trim());
                moive.setActor(textBox3.Text.Trim());
                moive.setDirector(textBox2.Text.Trim());
                moive.setFare((float)Convert.ToDouble(textBox6.Text.Trim()));
                moive.setLength(Convert.ToInt16(textBox7.Text.Trim()));
                moive.setPutDate(Convert.ToDateTime(textBox5.Text.Trim()));
                moive.setType(comboBox1.SelectedItem.ToString().Trim());


                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                    File.Copy(@mimage, @imagePath);
                }
                else
                {
                    File.Copy(@mimage, @imagePath);
                }

                moive.setImage(imagePath);
                moive.setStory(filePath);

                SqlMoive.addMoive(moive);

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

        public void Combox_Load()
        {
            comboBox1.Items.Clear();

            DataSet ds1 = SqlMoive.getAllMoiveType();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds1.Tables[0].Rows[i][0].ToString().Trim());
            }
        }

        private void MoiveAdd_Load(object sender, EventArgs e)
        {
            Combox_Load();
        }

    }
}
