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

namespace WindowsFormsApplication1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        static string connStr = null;
            //"Data Source=.;Initial Catalog=CinemaData;Integrated Security=True";

        public static void constr()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"resource\conf.xml");
            XmlElement xmlRoot = xmlDoc.DocumentElement;
            connStr = xmlRoot.ChildNodes.Item(0).InnerText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            constr();
            //使用SqlConnection 来连接数据库
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //创建sql 查询语句
                string sql = "select Password from Administrator where ID ='" + textBox1.Text + "'";
                //创建 SqlCommand 执行指令
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    //打开数据库连接
                    conn.Open();
                    //使用 SqlDataReader 来 读取数据库 
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        //SqlDataReader 在数据库中为 从第1条数据开始 一条一条往下读
                        if (sdr.Read())
                        {
                            //则将第1条 密码 赋给 字符串pwd  ,并且依次往后读取 所有的密码
                            //Trim()方法为移除字符串前后的空白
                            string pwd = sdr.GetString(0).Trim();
                            //如果 文本框中输入的密码 ==数据库中的密码
                            if (pwd == textBox2.Text)
                            {
                                //说明在该账户下 密码正确, 系统登录成功
                                MessageBox.Show("系统登录成功,正在跳转主页面...");
                                Cinema cinemaView = new Cinema();
                                cinemaView.Show();
                                this.Hide();
                            }
                            else
                            {
                                //否则密码错误 再次输入密码
                                MessageBox.Show("密码错误!请再次输入!");
                                //并自动将当前密码 清空
                                textBox2.Text = "";
                            }
                        }

                        else
                        {
                            //如果读取账户数据失败, 则用户名不存在
                            MessageBox.Show("用户名不存在,请重新出入!");
                            //并自动清空账户名
                            textBox2.Text = "";
                            textBox1.Text = "";
                        }
                    }
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register registerView = new Register();
            registerView.Show();
            this.Hide();
        }
    }
}
