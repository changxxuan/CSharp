using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //矩阵大小
        private int boundParaHeight = 30;//为了与二维数组的常量区别开，作为重新开始游戏的参数
        private int boundParaWidth = 16;//为了与二维数组的常量区别开，作为重新开始游戏的参数
        private const int initialHeight = 30;
        private const int initialWidth = 16;
        private static int[,] bounds = new int[initialHeight, initialWidth];//存放雷的数组
        private static Bound[,] bound;
        private static int Max_Bound_Count = 99;//炸弹的总量
        private static int ClearBounds = 0;//成功清除炸弹数量
        private static int num = 0;//显示标志的数量，与ClearBounds不同，ClearBounds只有是雷才会加减，num只要标记就加减
        private static DateTime startTime;//游戏开始时间 

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(790, 540);
            //panel1.Size = new Size(25 * quartWidth, 25 * quartHeight);
            panel1.Size = new Size(750,400);
            timer1.Interval = 1000;
            timer1.Enabled = true;
            startTime = DateTime.Now;
            label1.BackColor = Color.Blue;
            label1.ForeColor = Color.Red;
            label1.Font = new Font("隶书", 15F, FontStyle.Bold);
            label2.BackColor = Color.Blue;
            label2.ForeColor = Color.Red;
            label2.Font = new Font("隶书", 15F, FontStyle.Bold);

            GameSetting(initialHeight, initialWidth);
        }

        //左击排雷
        private void Btn_Click(object sender, EventArgs e)
        {
            Bound vbound = (Bound)sender;
            //如果不是未翻开状态，则退出
            if (vbound.Flag != 0)
                return;
            vbound.Flag = 1;
            vbound.FlatStyle = FlatStyle.Flat;

            if (vbound.Status == 0)
            {
                //bound.BackColor = System.Drawing.Color.Red;
                //当前的周围没有雷，就进行递归的排雷
                if (vbound.Nabar == 0)
                {
                    vbound.BackColor = System.Drawing.Color.White;

                    int i = vbound.X;
                    int j = vbound.Y;

                    if (i - 1 >= 0 && j - 1 >= 0) Btn_Click(bound[i - 1, j - 1], e);//左上
                    if (i - 1 >= 0) Btn_Click(bound[i - 1, j], e);//上
                    if (j - 1 >= 0) Btn_Click(bound[i, j - 1], e);//左
                    if (i + 1 <= boundParaHeight - 1 && j - 1 >= 0) Btn_Click(bound[i + 1, j - 1], e);//左下
                    if (i + 1 <= boundParaHeight - 1) Btn_Click(bound[i + 1, j], e);//下
                    if (i - 1 >= 0 && j + 1 <= boundParaWidth - 1) Btn_Click(bound[i - 1, j + 1], e);//右上
                    if (j + 1 <= boundParaWidth - 1) Btn_Click(bound[i, j + 1], e);//右
                    if (i + 1 <= boundParaHeight - 1 && j + 1 <= boundParaWidth - 1) Btn_Click(bound[i + 1, j + 1], e);//右下
                }
                else
                {
                    //vbound.Text = vbound.Nabar.ToString();
                    vbound.ShowNabar();
                }
            }
            else if (vbound.Status == 1)
            {
                //vbound.BackColor = System.Drawing.Color.Red;
                vbound.BackgroundImage = imageList1.Images[0];
                vbound.BackgroundImageLayout = ImageLayout.Stretch;
                ShowLose(boundParaHeight, boundParaWidth);
            }
            CheckGame(boundParaHeight, boundParaWidth);
        }

        //扫雷事件
        private void BtnRight_Click(object sender, MouseEventArgs e)
        {
            Bound vbound = (Bound)sender;
            //Debug.WriteLine(vbound.Size);
            //右击鼠标
            if (e.Button == MouseButtons.Right)
            {
                //如果已经翻开过，则返回
                if (vbound.Flag == 1)
                    return;
                //未翻开
                
                if (vbound.Flag == 0)
                {
                    if (num == Max_Bound_Count)
                        return;
                    //vbound.BackColor = System.Drawing.Color.Red;
                    vbound.BackgroundImage = imageList1.Images[1];
                    vbound.BackgroundImageLayout = ImageLayout.Stretch;
                    label1.Text = (++num).ToString();
                    label2.Text = (Max_Bound_Count - num).ToString();
                    if (vbound.Status == 1)
                    {
                        ClearBounds++;
                    }
                    vbound.Flag = 2;
                }
                else
                {
                    label1.Text = (--num).ToString();
                    label2.Text = (Max_Bound_Count - num).ToString();
                    Image img = null;
                    vbound.BackgroundImage = img;
                    if (vbound.Status == 1)
                    {
                        ClearBounds--;
                    }
                    vbound.Flag = 0;
                }
                CheckGame(boundParaHeight, boundParaWidth);
            }
        }
        //判断游戏状态
        private void CheckGame(int paraHeight, int paraWidth)
        {
            //清除所有雷
            if (ClearBounds == Max_Bound_Count)
            {
                //所有雷排完
                for (int i = 0; i < paraHeight; i++)
                    for (int j = 0; j < paraWidth; j++)
                    {
                        if(bound[i, j].Flag == 0)
                        {
                            return;
                        }
                    }
                ShowWin(paraHeight, paraWidth);               
            }
            //toolStripStatusLabel1.Text = ClearBounds.ToString() + "/" + Max_Bound_Count.ToString();
        }
        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;
            ReStartGame(boundParaHeight, boundParaWidth);
            //Debug.WriteLine(boundParaHeight.ToString());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan();
            ts = DateTime.Now.Subtract(startTime);
            toolStripStatusLabel1.Text = DateTime.Parse(ts.ToString()).ToString("HH:mm:ss");
        }

        private void CreatBounds(int paraHeight, int paraWidth)
        {
            int bound_count;
            Random rnd = new Random();
            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    bounds[i, j] = 0;
                }

            bound_count = 0;
            //Debug.WriteLine(Max_Bound_Count.ToString());
            while (bound_count < Max_Bound_Count)
            {
                int i = rnd.Next(paraHeight);
                int j = rnd.Next(paraWidth);

                if (bounds[i, j] == 0)
                {
                    bounds[i, j] = 1;
                    bound_count++;
                }
            }
            //Debug.WriteLine(bound_count.ToString());
        }

        private void CountNabar(int paraHeight, int paraWidth)
        {
            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    if (bounds[i, j] == 1)
                    {
                        if (i - 1 >= 0 && j - 1 >= 0) bound[i - 1, j - 1].Nabar++;
                        if (j - 1 >= 0) bound[i, j - 1].Nabar++;
                        if (i - 1 >= 0) bound[i - 1, j].Nabar++;
                        if (i + 1 <= paraHeight - 1 && j - 1 >= 0) bound[i + 1, j - 1].Nabar++;
                        if (i + 1 <= paraHeight - 1) bound[i + 1, j].Nabar++;
                        if (i - 1 >= 0 && j + 1 <= paraWidth - 1) bound[i - 1, j + 1].Nabar++;
                        if (j + 1 <= paraWidth - 1) bound[i, j + 1].Nabar++;
                        if (i + 1 <= paraHeight - 1 && j + 1 <= paraWidth - 1) bound[i + 1, j + 1].Nabar++;
                    }
                }
        }

        private void ShowWin(int paraHeight, int paraWidth)
        {
            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    if (bound[i, j].Status == 1)
                    {
                        bound[i,j].BackgroundImage = imageList1.Images[0];
                        bound[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            MessageBox.Show("you win!");
            ReStartGame(boundParaHeight, boundParaWidth);
        }

        private void ShowLose(int paraHeight, int paraWidth)
        {
            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    if (bound[i, j].Status == 1)
                    {
                        bound[i, j].BackgroundImage = imageList1.Images[0];
                        bound[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        if (bound[i, j].Flag == 2)
                        {
                            bound[i, j].BackgroundImage = imageList1.Images[2];
                            bound[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            MessageBox.Show("you lose!");
            ReStartGame(boundParaHeight, boundParaWidth);
        }

        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new Size(265, 365);
            panel1.Size = new Size(225, 225);
            //this.Size = new Size(790, 540);//用于测试：ReStartGame没加新功能 一直用的是初始化雷阵bound，变成中低级
            //panel1.Size = new Size(750, 400);//由于pannel和form大小的设置使其只显示当前级别的雷阵
            const int boundHeight = 9;
            const int boundWidth = 9;
            Max_Bound_Count = 10;
            boundParaHeight = boundHeight;
            boundParaWidth = boundWidth;
            bounds = new int[boundHeight, boundWidth];
            ReStartGame(boundHeight, boundWidth);
            //GameSetting(boundHeight, boundWidth);
        }

        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new Size(440, 540);
            panel1.Size = new Size(400, 400);
            const int boundHeight = 16;
            const int boundWidth = 16;
            Max_Bound_Count = 40;
            boundParaHeight = boundHeight;
            boundParaWidth = boundWidth;
            bounds = new int[boundHeight, boundWidth];
            ReStartGame(boundHeight, boundWidth);
            //GameSetting(boundHeight, boundWidth);
        }

        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Size = new Size(790, 540);
            panel1.Size = new Size(750, 400);
            const int boundHeight = 30;
            const int boundWidth = 16;
            Max_Bound_Count = 99;
            boundParaHeight = boundHeight;
            boundParaWidth = boundWidth;
            bounds = new int[boundHeight, boundWidth];
            ReStartGame(boundHeight, boundWidth);
            //GameSetting(boundHeight, boundWidth);
            //GameSetting(initialHeight, initialWidth);
        }

        private void ReStartGame(int paraHeight, int paraWidth)
        {
            //清除的雷数清0
            ClearBounds = 0;
            num = 0;
            label1.Text = (0).ToString();
            label2.Text = (Max_Bound_Count - num).ToString();
            startTime = DateTime.Now;
            //产生雷阵数组
            CreatBounds(paraHeight, paraWidth);

            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    bound[i, j].Nabar = 0;
                    bound[i, j].Status = bounds[i, j];
                    bound[i, j].Flag = 0;
                    bound[i, j].FlatStyle = FlatStyle.Standard;
                    bound[i, j].BackColor = System.Drawing.SystemColors.Control;
                    bound[i, j].Text = "";
                    Image img = null;
                    bound[i, j].BackgroundImage = img;
                }
            //计算周围雷的数量
            CountNabar(paraHeight, paraWidth);
        }


        private void GameSetting(int paraHeight, int paraWidth)
        {
            label1.Text = 0.ToString();
            label2.Text = Max_Bound_Count.ToString();

            //产生雷阵数组
            CreatBounds(paraHeight, paraWidth);

            bound = new Bound[paraHeight, paraWidth];
            //初始化所有雷
            for (int i = 0; i < paraHeight; i++)
                for (int j = 0; j < paraWidth; j++)
                {
                    bound[i, j] = new Bound(i, j);//=======================是不是可以省略掉,NO
                    bound[i, j].Status = bounds[i, j];
                    //面板的宽度不变，修改每个雷的长和宽
                    //bound[i, j].Width = panel1.Width / quartWidth;
                    //bound[i, j].Height = panel1.Height / quartHeight;
                    bound[i, j].Width = 25;
                    bound[i, j].Height = 25;

                    float currentSize;
                    currentSize = bound[i, j].Font.Size;
                    currentSize = 12F;
                    bound[i, j].Font = new Font(bound[i, j].Font.Name, currentSize, bound[i, j].Font.Style, bound[i, j].Font.Unit);
                    //分布雷的位置
                    bound[i, j].Left = i * bound[i, j].Width;
                    bound[i, j].Top = j * bound[i, j].Height;
                    //bound[i, j].Text = i.ToString();

                    this.panel1.Controls.Add(bound[i, j]);
                    //扫雷事件注册
                    bound[i, j].Click += new EventHandler(Btn_Click);
                    bound[i, j].MouseUp += new MouseEventHandler(BtnRight_Click);

                }
            //计算周围雷的数量
            CountNabar(paraHeight, paraWidth);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "继续")
            {
                button1.Text = "暂停";
                timer1.Enabled = true;
                for (int i = 0; i < boundParaHeight; i++)
                    for (int j = 0; j < boundParaWidth; j++)
                    {
                        bound[i, j].Enabled = true;
                    }
                return;
            }
            button1.Text = "继续";
            timer1.Enabled = false;
            for (int i = 0; i < boundParaHeight; i++)
                for (int j = 0; j < boundParaWidth; j++)
                {
                    bound[i, j].Enabled = false;
                }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
