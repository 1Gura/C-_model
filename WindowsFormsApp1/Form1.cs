using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sutki; i += h)
            {
                ///Тут должны поступать задания на каждый терминал с учётом времени t1, t2, t3
                foreach (Terminal terminal in terminals)
                {
                    terminal.interval -= h;
                    if (terminal.interval <= 0)
                    {
                        if (terminal.Task == null)
                        {
                            var task = new Task();
                            terminal.Task = task;
                        }
                        else
                        {
                            evm.stash.Add(new Task());
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
                                    textBox13.Text += "Поместили в СТЭШ";
                                }
                                terminals[0].Task = null;
                                textBox13.Text += "Задача решена/убрана из терминала";

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
                if (sutki <= 0)
                {
                    label7.Text = sutki.ToString();
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
    }
}
