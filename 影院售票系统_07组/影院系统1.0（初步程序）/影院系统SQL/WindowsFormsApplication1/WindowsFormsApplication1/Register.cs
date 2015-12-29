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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;
            string againPw = textBox3.Text;

            
            string check = "select ID from Administrator";

            DataSet ds = DbHelperSQL.Query(check, "ID");

            if (id != "" && password != "" && againPw != "")
            {
                foreach (DataRow row in ds.Tables["ID"].Rows)
                {
                    if(row["ID"].ToString().Trim()==id.Trim())
                    {
                        MessageBox.Show("该用户名已被占用，请重新输入");
                        textBox1.Text = null;
                        return;
                    }
                }
                if (password != againPw)
                {
                    MessageBox.Show("请确保两段密码相同.");
                }
                else
                {
                    
                    string insert = "insert into Administrator(ID,Password) values ('" + id + "','" + password + "')";
                    DbHelperSQL.ExecuteSql(insert);

                    MessageBox.Show("注册成功.");
                    Login loginView = new Login();
                    loginView.Show();
                    this.Dispose();
                }
            }
            else
            {
                MessageBox.Show("该输入所有数据.");
            }

        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

    }
}
