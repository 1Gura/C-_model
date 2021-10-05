using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private string writePath = @"test.txt";


        private int countTask = 0;
        private int countStash = 0;

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
            int countGlobal = 0;

            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("");
            }

            for (j = 3; j < sutki; j += h)
            {
                setTasks();
                if (terminals[0].Task != null || terminals[1].Task != null || terminals[2].Task != null)
                {

                    /// Тут идет обработка терминалом задачи, если задача успела выполнится,
                    /// то просто переходим к след. терминалу(или если ее нет),
                    /// иначе, если закончилось отведенное время поместим недоделанную задачу в СТЭШ
                    /// и перейдем к след. терминалу

                    int workOver = 0;
                    int timeOver = 0;
                    countGlobal++;
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
                                    fileWrite("Задача была решена на 1 терминале" + " на " + j);
                                    countTask++;

                                }
                                else
                                {
                                    evm.stash.Add(terminals[0].Task);
                                    fileWrite("Задача была помещена в очередь" + " на " + j);
                                    countStash++;

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
                                    fileWrite("Задача была решена на 2 терминале" + " на " + j);
                                    countTask++;

                                }
                                else
                                {
                                    evm.stash.Add(terminals[1].Task);
                                    fileWrite("Задача была помещена в очередь" + " на " + j);
                                    countStash++;

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
                                    fileWrite("Задача была решена на 3 терминале" + " на " + j);
                                    countTask++;

                                }
                                else
                                {
                                    evm.stash.Add(terminals[2].Task);
                                    fileWrite("Задача была помещена в очередь)" + " на " + j);
                                    countStash++;

                                }
                                terminals[2].Task = null;
                                k = 1;
                            }
                            break;
                    }
                }
                else
                {
                    if(evm.stash.Count() > 0)
                    {
                        terminals[k].Task = evm.stash.FirstOrDefault();
                        fileWrite("Задача была помещена в терминал из очереди" + " на " + j);
                    }
                    
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
        /// <summary>
        ///Тут должны поступать задания на каждый терминал с учётом времени t1, t2, t3
        /// </summary>
        private void setTasks()
        {
            var count = 0;
            foreach (Terminal terminal in terminals)
            {
                terminal.interval -= h;
                if (terminal.interval <= 0)
                {
                    count++;
                    if (terminal.Task == null)
                    {
                        var task = new Zadacha();
                        terminal.Task = task;
                        fileWrite(" " + "Задача поступила в терминал" + "№" + count + " на " + j);
                    }
                    else
                    {
                        evm.stash.Add(new Zadacha());
                        fileWrite("Задача была помещана в очередь т.к. Т" + count + "занят");
                        countStash++;
                            
                    }
                    terminal.interval = 30;
                }
            }
        }

        private void fileWrite(string str = "")
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true))
                {
                    sw.WriteLine(str);
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
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
            textBox7.Text = countTask.ToString();
            textBox8.Text = countStash.ToString();
        }
    }
}
