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
            string sname=textBox3.Text;
            string row=textBox1.Text;
            string col=textBox2.Text;
            string simage=pictureBox1.ImageLocation;

            string insert = "insert into Screens(SName,Row,col,SImage) values('"+sname+"',"+row+","+col+",'"+simage+"')";
            DbHelperSQL.ExecuteSql(insert);

            MessageBox.Show("添加成功");

            cinema.Cinema_Load(null, null);
           

            this.Dispose();
        }
    }
}
