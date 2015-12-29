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

                Moive moive = SqlMoive.getMoive(beSelect);

                label21.Text = moive.getName();
                label5.Text = moive.getType();
                label6.Text = moive.getDirector();
                label11.Text = moive.getActor();
                label4.Text = Convert.ToString(moive.getPutDate());
                label3.Text = Convert.ToString(moive.getLength());
                label15.Text = Convert.ToString(moive.getFare());

                if (moive.getImage() != null)
                {
                    pictureBox2.ImageLocation = @moive.getImage().Trim();
                }
                if (moive.getStory() != null)
                {
                    FileStream myfs = new FileStream(moive.getStory(), FileMode.Open, FileAccess.Read);
                    StreamReader mySr = new StreamReader(myfs, Encoding.Default);
                    textBox1.Text = mySr.ReadToEnd();
                    mySr.Close();
                    myfs.Close();
                }
            }
        }

        //影片 点击刷新时事件
        public void button3_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            DataSet ds = SqlMoive.getAllMoiveName();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
        }

        //售票 刷新
        private void showTree_Click(object sender, EventArgs e)
        {
            this.tvMovies.Nodes.Clear();

            DataSet ds1 = SqlPutPlan.getAllMoiveName();
            foreach (DataRow row1 in ds1.Tables[0].Rows)
            {
                TreeNode N1 = new TreeNode();
                N1.Name = row1[0].ToString().Trim();
                N1.Text = row1[0].ToString().Trim();
                this.tvMovies.Nodes.Add(N1);

                DataSet ds2 = SqlPutPlan.getTimeByMoiveName(N1.Name);
                foreach (DataRow row2 in ds2.Tables[0].Rows)
                {
                    TreeNode N2 = new TreeNode();
                    N2.Name = row2[0].ToString().Trim();
                    N2.Text = row2[0].ToString().Trim();
                    N1.Nodes.Add(N2);
                }
            }
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

                Screens screen = SqlScreens.getScreenBySName(beSelect);

                pictureBox3.Image = Image.FromFile(screen.getImage());
                InitSeats(screen.getRow(),screen.getCol(), "tabControl5");

            }
        }

        //放映厅 点击刷新按钮事件
        public void button4_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            DataSet ds = SqlScreens.getAllScreensName();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                listBox2.Items.Add(ds.Tables[0].Rows[i][0].ToString().Trim());
            }
                ;
        }


       // treeview 选择的内容改变
        private void tvMovies_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (this.tvMovies.SelectedNode.Level == 1)
            {

                tpCinema.Controls.Clear();
                string moiveName = this.tvMovies.SelectedNode.Parent.Text;
                Moive moive = SqlMoive.getMoive(moiveName);

                string time = this.tvMovies.SelectedNode.Text;

                lblMovieName.Text = moive.getName();
                lblTime.Text = time;


                //显示电影信息
                lblType.Text = moive.getType();
                lblDirector.Text = moive.getDirector();
                lblActor.Text = moive.getActor();
                lblPrice.Text = Convert.ToString(moive.getFare());
                string image = moive.getImage();
                picMovie.Image = Image.FromFile(image);
                if (rdoNormal.Checked)
                {
                    lblCalcPrice.Text = lblPrice.Text;
                }
                //显示座位信息
                PutPlan putPlan = SqlPutPlan.getSname(moive.getName(), time);
                Screens screen = SqlScreens.getScreenBySName(Convert.ToString(putPlan.getScreenName()));
                tpCinema.Text = putPlan.getScreenName() + "号厅";
                InitSeats(screen.getRow(), screen.getCol(), "tpCinema");

                //查询已售出座位并置其为红色

                DataSet ds1 = SqlBook.getSeatByKye(screen.getName(), putPlan.getMoiveName(), time);
                foreach (DataRow row1 in ds1.Tables[0].Rows)
                {
                    string ST = row1[0].ToString();
                    foreach (Control ct in tpCinema.Controls)
                    {
                        if (ct is Label)
                        {
                            if (ct.Text == ST.Trim()) ct.BackColor = Color.Red;
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
            
            string MName = lblMovieName.Text;
            string time = lblTime.Text;
            string seat = lbl.Text;

            Book book = new Book();
            book.setMoiveName(MName);
            book.setTime(Convert.ToDateTime(time));
            book.setSeat(seat);
            
            PutPlan putPlan = SqlPutPlan.getSname(MName, time);

            book.setScreenName(putPlan.getScreenName());
            if (lbl.BackColor == Color.Red)
            {
                if (DialogResult.OK == MessageBox.Show("是否退票", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    SqlBook.refund(book);
                    lbl.BackColor = Color.Yellow;
                } 
            }
            else
            {
                if (DialogResult.OK == MessageBox.Show("是否购买", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {

                    lbl.BackColor = Color.Red;

                    //购票信息存入数据库                    
                    book.setAfterDiscount((float)Convert.ToDouble(lblCalcPrice.Text));
                    book.setSaleTime(Convert.ToDateTime(DateTime.Now.ToLocalTime().ToString()));
                    if (rdoNormal.Checked) 
                    { 
                        book.setConsumerType("normal");
                        book.setDiscount(0);
                    }
                    else if (rdoFree.Checked) 
                    {
                        book.setConsumerType("free");
                        book.setDiscount(0);
                    }
                    else 
                    { 
                        book.setConsumerType("student");
                        book.setDiscount((float)Convert.ToDouble(this.cmbDisCount.SelectedItem.ToString()));
                    }

                    SqlBook.buyTicket(book);

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

            DataSet ds1 = SqlPutPlan.getAllScreensName();
            foreach (DataRow row1 in ds1.Tables[0].Rows)
            {
                TreeNode N1 = new TreeNode();
                N1.Name = row1[0].ToString().Trim();
                N1.Text = row1[0].ToString().Trim();
                this.treeView1.Nodes.Add(N1);

                DataSet ds2 = SqlPutPlan.getMoiveNameByScreenName(N1.Name.Trim());
                foreach (DataRow row2 in ds2.Tables[0].Rows)
                {
                    TreeNode N2 = new TreeNode();
                    N2.Name = row2[0].ToString().Trim();
                    N2.Text = row2[0].ToString().Trim();
                    N1.Nodes.Add(N2);

                    DataSet ds3 = SqlPutPlan.getTimeByMoiveScressName(N2.Name.Trim(), N1.Name.Trim());
                    foreach (DataRow row3 in ds3.Tables["time"].Rows)
                    {
                        TreeNode N3 = new TreeNode();
                        N3.Name = row3[0].ToString().Trim();
                        N3.Text = row3[0].ToString().Trim();
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
                DateTime time = Convert.ToDateTime(textBox5.Text);

                PutPlan putPlan = new PutPlan(mname,sname,time);

                SqlPutPlan.putPlanAdd(putPlan);


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
                DateTime time = Convert.ToDateTime(treeView1.SelectedNode.Text);

                PutPlan putPlan = new PutPlan(mname,sname,time);
                SqlPutPlan.deletePutPlan(putPlan);

                MessageBox.Show("删除成功");
            }
            else
            {
                MessageBox.Show("请选择你要删除的排片信息。");
            }

        }

        //点击右上角X退出程序
        private void Cinema_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //添加影片
        private void button1_Click(object sender, EventArgs e)
        {
            MoiveAdd moiveAdd = new MoiveAdd(this);
            moiveAdd.Show();
           

        }

        //影片删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItem != null)
            { 
                //尚且存在Bug,若点击删除，不点击刷新，此时点击被删除的那项会抛出异常
                SqlMoive.deleteMoive(listBox1.SelectedItem.ToString());

                this.Combox_Load();
                MessageBox.Show("删除成功");
                button4_Click(null, null);
                }

            else
            {
                MessageBox.Show("请选择你要删除的影片。");
            }
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
            if (this.listBox2.SelectedItem != null)
            {
                //尚且存在Bug,若点击删除，不点击刷新，此时点击被删除的那项会抛出异常
                SqlScreens.deleteScress(listBox2.SelectedItem.ToString());

                this.Combox_Load();
                MessageBox.Show("删除成功");
            }
            else
            {
                MessageBox.Show("请选择你要删除的放映厅。");
            }
        }

        //查询 电影票查询
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                int allnumber, todaynumber;

                allnumber = SqlMoive.allMoiveNumber(comboBox3.SelectedItem.ToString());
                todaynumber = SqlMoive.singleMoiveNumber(comboBox3.SelectedItem.ToString());

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

                int allnumber, todaynumber;

                allnumber = SqlScreens.allScreenNumber(Convert.ToInt16(comboBox4.SelectedItem.ToString()));
                todaynumber = SqlScreens.singleScreenNumber(Convert.ToInt16(comboBox4.SelectedItem.ToString()));

                textBox11.Text = Convert.ToString(allnumber);
                textBox8.Text = Convert.ToString(todaynumber);
            }
            else
            {
                MessageBox.Show("请选择你要查询的电影");
            }
        }

        //加载Combox
        public void Combox_Load()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();

            DataSet ds1 = SqlMoive.getAllMoiveName();
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                comboBox1.Items.Add(ds1.Tables[0].Rows[i][0].ToString().Trim());
                comboBox3.Items.Add(ds1.Tables[0].Rows[i][0].ToString().Trim());
            }

            DataSet ds2 = SqlScreens.getAllScreensName();
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                comboBox2.Items.Add(ds2.Tables[0].Rows[i][0].ToString().Trim());
                comboBox4.Items.Add(ds2.Tables[0].Rows[i][0].ToString().Trim());
            }

            comboBox1.Text = "请选择";
            comboBox2.Text = "请选择";
            comboBox3.Text = "请选择";
            comboBox4.Text = "请选择";
        }

        //主界面初始化加载
        private void Cinema_Load(object sender, EventArgs e)
        {
            this.Combox_Load();
            button3_Click(null, null);
            button4_Click(null, null);
            button11_Click(null, null);
            showTree_Click(null, null);
            listBox1.SetSelected(0, true);
            listBox2.SetSelected(0, true);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }



    }
}
