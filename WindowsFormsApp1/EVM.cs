using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class EVM
    {
        /// <summary>
        /// время смены терминала = 30
        /// </summary>
        private int time = 30;

        /// <summary>
        /// Буффер
        /// </summary>
        public List<Task> stash = new List<Task>();

        public int[] Work(Terminal terminal, int h)
        {
            int[] mas = new int [2];
            mas[0] = terminal.Task.N - terminal.M * h;
            mas[1] = terminal.timeWork - h;
            return mas;
        }

        public void timeReset(int time = 30)
        {
            this.time = time;
        }


    }
}
