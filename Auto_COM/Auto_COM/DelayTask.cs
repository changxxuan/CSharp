using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auto_COM
{
	public class DelayTask
    {
        //public bool IsRun{get; set;} = true;
        private bool isRun = true;
        public bool IsRun
        {
            get
            {
                return isRun;
            }
            set
            {
                isRun = value;
            }
        }
		public int DelayTime{get; set;}
		public Action Action{get; set;}
    }
}
