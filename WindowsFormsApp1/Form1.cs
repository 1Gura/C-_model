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
            graph.DrawLine(pen, 10,50,150,200);
            pictureBox2.Image = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var terminal1 = new Terminal();
            var terminal2 = new Terminal();
            var terminal3 = new Terminal();
            var task = new Task();
            var evm = new EVM();
            int k = 1;

            for (int i = 0; i < sutki; i+=h)
            {
                switch(k)
                {
                    case 1 :
                        if (evm.Work(task, terminal1, h)[0] <= 0 && evm.Work(task, terminal1, h)[1] > 0)
                        {
                            evm.timeReset();
                            k = 2;
                        }
                        break;
                    case 2:
                        if (evm.Work(task, terminal2 , h)[0] <= 0 && evm.Work(task, terminal2 , h)[1] > 0)
                        {
                            terminal1 = new Terminal();
                            k = 3;
                        }
                        break;
                    case 3:
                        if (evm.Work(task, terminal3, h)[0] <= 0 && evm.Work(task, terminal3, h)[1] > 0)
                        {
                            terminal1 = new Terminal();
                            k = 1;
                        }
                        break;
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
