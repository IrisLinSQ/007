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
using ClassUse;

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
            string id = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();
            string againPw = textBox3.Text.Trim();

            if (id != "" && password != "" && againPw != "")
            {
                if (SqlAdmin.exitById(id))
                {
                    MessageBox.Show("该用户名已经存在。");
                    return;
                }
                else if (password != againPw)
                {
                    MessageBox.Show("请确保两段密码相同.");
                }
                else
                {
                    Admin admin = new Admin(id,password);
                    SqlAdmin.addAdmin(admin);
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
