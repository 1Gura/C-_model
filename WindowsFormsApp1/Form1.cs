using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int sutki = 24 * 60 * 60;
        private int t1 = 30;
        private int t2 = 30;
        private int t3 = 30;
        private int t4 = 30;
        private int N = 60;
        private int M = 3;
        private int h = 3;
        private int j = 0;
        private List<Terminal> terminals = new List<Terminal> { new Terminal(), new Terminal(), new Terminal() };
        EVM evm = new EVM();
        int k = 1;
        public Form1()
        {
            InitializeComponent();
            this.Draw();
        }
        public void Draw()
        {
            var bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics graph = Graphics.FromImage(bmp);
            var pen = new Pen(Color.Blue);
            graph.DrawLine(pen, 10, 50, 150, 200);
            pictureBox2.Image = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private async void button2_ClickAsync(object sender, EventArgs e)
        {
            timer1.Start();

            await Task.Run(() =>
            {
                Start();
            });

        }

        private void Start()
        {
            for (j = 0; j < sutki; j += h)
            {
                ///Тут должны поступать задания на каждый терминал с учётом времени t1, t2, t3
                foreach (Terminal terminal in terminals)
                {
                    terminal.interval -= h;
                    if (terminal.interval <= 0)
                    {
                        if (terminal.Task == null)
                        {
                            var task = new Zadacha();
                            terminal.Task = task;
                        }
                        else
                        {
                            Thread.Sleep(1);
                            evm.stash.Add(new Zadacha());
                        }
                        terminal.interval = 30;
                    }
                }
                if (terminals[0].Task != null || terminals[1].Task != null || terminals[2].Task != null)
                {

                    /// Тут идет обработка терминалом задачи, если задача успела выполнится,
                    /// то просто переходим к след. терминалу(или если ее нет),
                    /// иначе, если закончилось отведенное время поместим недоделанную задачу в СТЭШ
                    /// и перейдем к след. терминалу

                    int workOver = 0;
                    int timeOver = 0;

                    switch (k)
                    {
                        case 1:
                            workOver = (evm.Work(terminals[0], h));
                            timeOver = evm.time -= h;
                            if (workOver <= 0 || timeOver <= 0)
                            {
                                evm.timeReset();
                                if (workOver <= 0)
                                {
                                    evm.stash.Add(terminals[0].Task);
                                }
                                terminals[0].Task = null;

                                k = 2;
                            }
                            break;
                        case 2:
                            workOver = (evm.Work(terminals[1], h));
                            timeOver = evm.time -= h;
                            if (workOver <= 0 || timeOver <= 0)
                            {

                                evm.timeReset();
                                if (workOver <= 0)
                                {
                                    evm.stash.Add(terminals[1].Task);
                                }
                                terminals[1].Task = null;
                                k = 3;
                            }
                            break;
                        case 3:
                            workOver = (evm.Work(terminals[2], h));
                            timeOver = evm.time -= h;
                            if (workOver <= 0 || timeOver <= 0)
                            {

                                evm.timeReset();
                                if (workOver <= 0)
                                {
                                    evm.stash.Add(terminals[2].Task);
                                }
                                terminals[2].Task = null;
                                k = 3;
                            }
                            break;
                    }
                }
                else
                {
                    terminals[1].Task = evm.stash.FirstOrDefault();
                    k = 1;
                }
                if (j >= sutki)
                {
                    timer1.Stop();
                    return;
                }

            }

        }

        private void setTerminal(bool workOver)
        {
            evm.timeReset();
            if (!workOver)
            {
                evm.stash.Add(terminals[1].Task);
            }
            terminals[1].Task = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label7.Text = k.ToString();
        }

        private void process1_Exited(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox13.Text = j.ToString();
        }
    }
}
