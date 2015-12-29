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
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Cinema : Form
    {
        public Cinema()
        {
            InitializeComponent();
        }

        Label lbl = new Label();
        
        //影片listbox控件发生改变
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            {
                
                string beSelect = listBox1.SelectedItem.ToString();
               
                label21.Text = listBox1.SelectedItem.ToString();
                label5.Text = (String)DbHelperSQL.GetSingle("select MType from Movie where MName='" + beSelect + "'");
                label6.Text = (String)DbHelperSQL.GetSingle("select MDirector from Movie where MName='" + beSelect + "'");
                label11.Text = (String)DbHelperSQL.GetSingle("select MActor from Movie where MName='" + beSelect + "'");
                label4.Text = Convert.ToString(DbHelperSQL.GetSingle("select MPutday from Movie where MName='" + beSelect + "'"));
                label3.Text = Convert.ToString((int)DbHelperSQL.GetSingle("select MLength from Movie where MName='" + beSelect + "'"));
                label15.Text = Convert.ToString((double)DbHelperSQL.GetSingle("select MFare from Movie where MName='" + beSelect + "'"));
                string imageLocation=(String)DbHelperSQL.GetSingle("select MImage from Movie where MName='" + beSelect + "'");
                if(imageLocation!=null)
                {
                    pictureBox2.ImageLocation = @imageLocation.Trim();
                }
                string storyPath = (string)DbHelperSQL.GetSingle("select MStory from Movie where MName='" + beSelect + "'");
                if (storyPath != null)
                {
                    FileStream myfs = new FileStream(storyPath, FileMode.Open, FileAccess.Read);
                    StreamReader mySr = new StreamReader(myfs, Encoding.Default);
                    textBox1.Text = mySr.ReadToEnd();
                    mySr.Close();
                    myfs.Close();
                }
            }
        }

        //影片 点击刷新时事件
        private void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            SqlDataReader myRead=DbHelperSQL.ExecuteReader("select MName from Movie");
            while (myRead.Read())
            {
                listBox1.Items.Add(myRead["MName"].ToString().Trim());
            }
            myRead.Close();
        }

        //售票 刷新
        private void showTree_Click(object sender, EventArgs e)
        {
            this.tvMovies.Nodes.Clear();

            DataSet ds1 = DbHelperSQL.Query("select  distinct MName from Put_Plan ","MName");
            foreach (DataRow row1 in ds1.Tables["MName"].Rows)
            {
                TreeNode N1 = new TreeNode();
                N1.Name = row1["MName"].ToString();
                N1.Text = row1["MName"].ToString();
                this.tvMovies.Nodes.Add(N1);

                DataSet ds2 = DbHelperSQL.Query("select  distinct time from Put_Plan where MName='" + N1.Name + "'", "time");
                foreach (DataRow row2 in ds2.Tables["time"].Rows)
                {
                    TreeNode N2 = new TreeNode();
                    N2.Name = row2["time"].ToString();
                    N2.Text = row2["time"].ToString();
                    N1.Nodes.Add(N2);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }


        //初始化座位
        private void InitSeats(int row, int col, string tcl)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Label lb = new Label();
                    lb.BackColor = Color.Yellow;
                    if (tcl == "tabControl5")
                    {
                        lb.Location = new Point(10 + j * 70, 25 + i * 35);
                        lb.Font = new Font("Courier New", 9);
                        lb.Size = new Size(50, 25);
                    }
                    if (tcl == "tpCinema")
                    {
                        lb.Location = new Point(20 + j * 110, 25 + i * 60);
                        lb.Font = new Font("Courier New", 11);
                        lb.Size = new Size(80, 30);
                    }

                    lb.Name = (i + 1) + "-" + (j + 1);

                    lb.TabIndex = 0;
                    lb.Text = (i + 1) + "-" + (j + 1);
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    if (tcl == "tabControl5")
                    {
                        tabControl5.Controls.Add(lb);
                    }
                    if (tcl == "tpCinema")
                    {
                        lb.Click += lb_Click;
                        tpCinema.Controls.Add(lb);
                    }

                }
            }
            if (tcl == "tpCinema")
            {
                Label view1 = new Label();
                view1.BackColor = Color.Yellow;
                view1.Location = new Point(694, 105);
                view1.Font = new Font("Courier New", 9);
                view1.Size = new Size(40, 25);
                view1.TabIndex = 0;
                view1.Text = "未售";
                view1.TextAlign = ContentAlignment.MiddleCenter;

                Label view2 = new Label();
                view2.BackColor = Color.Red;
                view2.Location = new Point(694, 206);
                view2.Font = new Font("Courier New", 9);
                view2.Size = new Size(40, 25);
                view2.TabIndex = 0;
                view2.Text = "已售";
                view2.TextAlign = ContentAlignment.MiddleCenter;

                tpCinema.Controls.Add(view1);
                tpCinema.Controls.Add(view2);
            }
        }


        //放映厅 listbox中发生改变事件
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                tabControl5.Controls.Clear();
                string beSelect = listBox2.SelectedItem.ToString();

                int row = (int)DbHelperSQL.GetSingle("select Row from Screens where SName='" + beSelect + "'");
                int col = (int)DbHelperSQL.GetSingle("select col from Screens where SName='" + beSelect + "'");
                string image = (string)DbHelperSQL.GetSingle("select SImage from Screens where SName='" + beSelect + "'");

                pictureBox3.Image = Image.FromFile(image);

                InitSeats(row, col, "tabControl5");

            }
        }

        //放映厅 点击刷新按钮事件
        private void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            SqlDataReader sdr = DbHelperSQL.ExecuteReader("select SName from Screens");
            while (sdr.Read())
            {
                listBox2.Items.Add(sdr["SName"].ToString().Trim());
            }
            sdr.Close();
        }


        //treeview 选择的内容改变
        private void tvMovies_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (this.tvMovies.SelectedNode.Level == 1)
            {
                tpCinema.Controls.Clear();
                string moive = this.tvMovies.SelectedNode.Parent.Text;
                string time = this.tvMovies.SelectedNode.Text;

                lblMovieName.Text = moive;
                lblTime.Text = time;


                //显示电影信息
      
                lblType.Text = (string)DbHelperSQL.GetSingle("select MType from Movie where MName='" + moive + "'");
                lblDirector.Text = (string)DbHelperSQL.GetSingle("select MDirector from Movie where MName='" + moive + "'");
                lblActor.Text = (string)DbHelperSQL.GetSingle("select MActor from Movie where MName='" + moive + "'");
                lblPrice.Text = Convert.ToString(DbHelperSQL.GetSingle("select MFare from Movie where MName='" + moive + "'"));
                string image = (string)DbHelperSQL.GetSingle("select MImage from Movie where MName='" + moive + "'");
                picMovie.Image = Image.FromFile(image);
                if (rdoNormal.Checked)
                {
                    lblCalcPrice.Text = lblPrice.Text;
                }
                //显示座位信息
         
                string sname = (string)DbHelperSQL.GetSingle("select SName from Put_Plan where MName='" + moive + "' and time='" + time + "'");
                int row = (int)DbHelperSQL.GetSingle("select Row from Screens where SName='" + sname + "'");
                int col = (int)DbHelperSQL.GetSingle("select col from Screens where SName='" + sname + "'");
                tpCinema.Text = sname.Trim() + "号厅";
                InitSeats(row, col, "tpCinema");

                //查询已售出座位并置其为红色

                DataSet ds1 = DbHelperSQL.Query("select seat from Book_record where SName='" + sname + "' and MName='" + moive + "'and time='" + time + "'","seat");
                foreach (DataRow row1 in ds1.Tables["seat"].Rows)
                {
                    string ST = row1["seat"].ToString();
                    foreach (Control ct in tpCinema.Controls)
                    {
                        if (ct is Label)
                        {
                            if (ct.Text == ST.Trim()) ct.BackColor=Color.Red;
                        }
                    }
                }
            }
        }

        //点击普通票
        private void rdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbDisCount.Enabled = false;
            lblCalcPrice.Text = lblPrice.Text;
        }
        //点击赠票
        private void rdoFree_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbDisCount.Enabled = false;
            lblCalcPrice.Text = Convert.ToString(0);

        }
        //点击学生票
        private void rdoStudent_CheckedChanged(object sender, EventArgs e)
        {
            this.cmbDisCount.Enabled = true;
        }
        //学生票折扣发生改变
        private void cmbDisCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lblPrice.Text != "" && this.cmbDisCount.SelectedItem.ToString() != null)
            {
                this.lblCalcPrice.Text = (Convert.ToDouble(this.lblPrice.Text) * Convert.ToDouble(this.cmbDisCount.SelectedItem.ToString()) / 10).ToString();
            }
        }
        //售票 点击座位表购票
        private void lb_Click(object sender, EventArgs e)
        {
            lbl = sender as Label;
            if (lbl.BackColor == Color.Red)
            {
                MessageBox.Show("已售出");
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("是否购买", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {

                    lbl.BackColor = Color.Red;

                    //购票信息存入数据库
                    string MName = lblMovieName.Text;
                    string time = lblTime.Text;
                    string seat = lbl.Text;
                    string ConsumerType;
                    float Discount;
                    float AfterDiscount = (float)Convert.ToDouble(lblCalcPrice.Text);
                    string SaleTime = DateTime.Now.ToLocalTime().ToString();
                    if (rdoNormal.Checked) { ConsumerType = "normal"; Discount = 0; }
                    else if (rdoFree.Checked) { ConsumerType = "free"; Discount = 0; }
                    else { ConsumerType = "student"; Discount = (float)Convert.ToDouble(this.cmbDisCount.SelectedItem.ToString()); }

                    int SName = Convert.ToInt16(DbHelperSQL.GetSingle("select SName from Put_Plan where MName='" + MName + "' and time='" + time + "'"));

                    string SQLStr = "Insert into Book_record Values('" + MName + "','" + time + "'," + SName + ",'" + seat + "','" + ConsumerType + "'," + Discount + "," + AfterDiscount + ",'" + SaleTime + "')";
                    DbHelperSQL.ExecuteSql(SQLStr);


                }
            }

        }


        //排片 treeview改变
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        //排片 刷新
        private void button11_Click(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();

            DataSet ds1 = DbHelperSQL.Query("select  distinct SName from Put_Plan ","SName");
            foreach (DataRow row1 in ds1.Tables["SName"].Rows)
            {
                TreeNode N1 = new TreeNode();
                N1.Name = row1["SName"].ToString();
                N1.Text = row1["SName"].ToString();
                this.treeView1.Nodes.Add(N1);

                DataSet ds2 = DbHelperSQL.Query("select  distinct MName from Put_Plan where SName='" + N1.Name + "'","MName");
                foreach (DataRow row2 in ds2.Tables["MName"].Rows)
                {
                    TreeNode N2 = new TreeNode();
                    N2.Name = row2["MName"].ToString();
                    N2.Text = row2["MName"].ToString();
                    N1.Nodes.Add(N2);

                    DataSet ds3 = DbHelperSQL.Query("select  distinct time from Put_Plan where MName='" + N2.Name + "'and SName='" + N1.Name + "'","time"); 
                    foreach (DataRow row3 in ds3.Tables["time"].Rows)
                    {
                        TreeNode N3 = new TreeNode();
                        N3.Name = row3["time"].ToString();
                        N3.Text = row3["time"].ToString();
                        N2.Nodes.Add(N3);
                    }

                }
            }



        }
        //排片重置
        private void button8_Click(object sender, EventArgs e)
        {
    
            textBox5.Text = null;
       
        }
        //排片添加
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                int sname = Convert.ToInt16(comboBox2.SelectedItem.ToString());
                string mname = comboBox1.SelectedItem.ToString();
                string time = textBox5.Text;

                string insert = "insert into Put_Plan(MName,SName,time) values ('" + mname + "'," + sname + ",'" + time + "')";
                DbHelperSQL.ExecuteSql(insert);

                MessageBox.Show("添加成功");

            }
            else 
            {
                MessageBox.Show("请选择完整数据");
            }
            
            
        }
        //排片删除
        private void button12_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent.Parent != null)
            {

                int sname =Convert.ToInt16(treeView1.SelectedNode.Parent.Parent.Text);
                string mname = treeView1.SelectedNode.Parent.Text;
                string time = treeView1.SelectedNode.Text;

                string insert = "delete from Put_plan where MName='" + mname + "' and SName='" + sname + "' and time = '" + time + "'";
                DbHelperSQL.ExecuteSql(insert);

                MessageBox.Show("删除成功");
            }

        }
        
        private void Cinema_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MoiveAdd moiveAdd = new MoiveAdd(this);
            moiveAdd.Show();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string insert = "delete from Movie where MName='"+listBox1.SelectedItem.ToString()+"'";
            DbHelperSQL.ExecuteSql(insert);

            this.Cinema_Load(null, null);
            MessageBox.Show("删除成功");
        }
        //放映厅添加
        private void button6_Click(object sender, EventArgs e)
        {
            ScreensAdd screenAdd = new ScreensAdd(this);
            screenAdd.Show();
        }
        //放映厅删除
        private void button5_Click(object sender, EventArgs e)
        {       
            string insert = "delete from Screens where SName='" + listBox2.SelectedItem.ToString() + "'";
            DbHelperSQL.ExecuteSql(insert);

            this.Cinema_Load(null, null);
            MessageBox.Show("删除成功");
        }
        //查询 电影票查询
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

                string moive = comboBox3.SelectedItem.ToString();
                int allnumber, todaynumber;
               
                allnumber = (int)DbHelperSQL.GetSingle("select count(*) from Book_record where MName ='" + comboBox3.SelectedItem.ToString() + "'");
                todaynumber = (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and MName ='" + comboBox3.SelectedItem.ToString() + "'");

                textBox6.Text = Convert.ToString(allnumber);
                textBox3.Text = Convert.ToString(todaynumber);
            }
            else
            {
                MessageBox.Show("请选择你要查询的电影");
            }
        }
        //查询 放映厅查询
        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                textBox11.Text = "";
                textBox8.Text = "";

                DateTime begin = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
                DateTime finish = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));

                string screens = comboBox4.SelectedItem.ToString();
                int allnumber, todaynumber;

                allnumber = (int)DbHelperSQL.GetSingle("select count(*) from Book_record where SName ='" + comboBox4.SelectedItem.ToString() + "'");
                todaynumber = (int)DbHelperSQL.GetSingle("select count(*) from Book_record where Saletime >= '" + begin + "' and time <='" + finish + "'and SName ='" + comboBox4.SelectedItem.ToString() + "'");

                textBox11.Text = Convert.ToString(allnumber);
                textBox8.Text = Convert.ToString(todaynumber);
            }
            else
            {
                MessageBox.Show("请选择你要查询的电影");
            }
        }
        //加载Combox
        public void Cinema_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            SqlDataReader sdr = DbHelperSQL.ExecuteReader("select MName from Movie ");
            while (sdr.Read()) 
            {
                
                comboBox1.Items.Add(sdr.GetString(0).Trim());
                comboBox3.Items.Add(sdr.GetString(0).Trim());

            }
            sdr.Close();

            SqlDataReader sdr1 = DbHelperSQL.ExecuteReader("select SName from Screens ");
            while (sdr1.Read())
            {
                
                comboBox2.Items.Add(sdr1.GetInt32(0).ToString());
                comboBox4.Items.Add(sdr1.GetInt32(0).ToString());

            }
            sdr1.Close();

            comboBox1.Text = "请选择";
            comboBox2.Text = "请选择";
            comboBox3.Text = "请选择";
            comboBox4.Text = "请选择";
            

        }

        
        
    }
}
