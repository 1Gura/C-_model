using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Terminal
    {
        /// <summary>
        /// Время на обработку задачи в секундакх
        /// </summary>
        public int timeWork = 30;
        /// <summary>
        /// Количество символов обрабатываемых за секунду
        /// </summary>
        public int M = 3;


        private Task task { get; set; }
        public Task Task
        {
            get
            {
                return this.task;
            }
            set
            {   
                if(value != null)
                {
                    this.task = value;
                }
            }
        }

        public Terminal(int timeWork = 3, int M = 30)
        {
            this.timeWork = timeWork;
            this.M = M;
        }

    }
}