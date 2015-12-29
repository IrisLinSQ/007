using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Data;
using System.Data.SqlClient;
using System.Xml;
using SqlHelper;
using ClassUse;

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin admin = SqlLogin.findAdminById(textBox1.Text.Trim());
            if (admin == null)
            {
                MessageBox.Show("用户名不存在,请重新输入!");
                textBox2.Text = "";
                textBox1.Text = "";
            }
            else if (admin.getPassword().Trim() == textBox2.Text.Trim())
            {
                String loginTime = DateTime.Now.ToLocalTime().ToString();
                ClassUse.Login login = new ClassUse.Login(admin.getID(), loginTime);

                SqlLogin.addLogin(login);

                MessageBox.Show("系统登录成功,正在跳转主页面...");
                Cinema cinemaView = new Cinema();
                cinemaView.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("密码错误!请再次输入!");
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register registerView = new Register();
            registerView.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox1.Text = "";
        }


    }
}
