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
        public int[] Work(Task task, Terminal evm, int h)
        {
            int[] mas = new int [2];
            mas[0] = task.N - evm.M * h;
            mas[1] = evm.timeWork - h;
            return mas;
        }

        public void timeReset(int time = 30)
        {
            this.time = time;
        }


    }
}
