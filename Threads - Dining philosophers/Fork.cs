using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads___Dining_philosophers
{
    internal class Fork
    {
        public int id;
        public object inUse = new object();
        //public bool inUse;

        public Fork(int nr)
        {
            this.id = nr;
        }

        public void LockFork()
        {
            inUse = Monitor.TryEnter(this);
        }
        public void UnlockFork()
        {
            Monitor.Exit(inUse);
            inUse = false;
        }
    }
}
