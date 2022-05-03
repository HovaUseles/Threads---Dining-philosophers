using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads___Dining_philosophers
{
    internal class Philosoph
    {
        public Fork leftFork;
        public Fork rightFork;
        public int id;
        public string name;
        public int spaghetti;
        public Thread thread;
        public bool runTread;
        public Philosoph(Fork right, Fork left, int seatNr, string givenName)
        {
            this.leftFork = left;
            this.rightFork = right;
            this.id = seatNr;
            this.name = givenName;
            thread = new Thread(this.EatCheck);
            thread.Name = "Philosopher " + seatNr;
            this.spaghetti = 1000;
            this.runTread = true;
        }

        public void EatCheck()
        {
            while (spaghetti > 0 && this.runTread == true)
            {
                if (Monitor.TryEnter(this.leftFork.inUse))
                {
                    TryOtherFork(this.leftFork);
                }
                if (Monitor.TryEnter(this.rightFork.inUse))
                {
                    TryOtherFork(this.rightFork);
                }

                #region Old Code
                //    bool leftAvailable;
                //    bool rightAvailable;

                //    if (this.id == 4)
                //    {
                //        leftAvailable = Monitor.TryEnter(this.leftFork.inUse);
                //        rightAvailable = Monitor.TryEnter(this.rightFork.inUse);
                //    }
                //    else
                //    {
                //        rightAvailable = Monitor.TryEnter(this.rightFork.inUse);
                //        leftAvailable = Monitor.TryEnter(this.leftFork.inUse);
                //    }


                //    if (leftAvailable && rightAvailable)
                //    {
                //        this.Eat();
                //    }
                //    else if (leftAvailable)
                //    {
                //        Monitor.Exit(this.leftFork.inUse);
                //        Console.WriteLine("Philosoph {0} is waiting. Thread [{1}]", this.id, Thread.CurrentThread.ManagedThreadId);
                //    }
                //    else if (rightAvailable)
                //    {
                //        Monitor.Exit(this.rightFork.inUse);
                //        Console.WriteLine("Philosoph {0} is waiting. Thread [{1}]", this.id, Thread.CurrentThread.ManagedThreadId);
                //    }
                //    else
                //    {
                //        Console.WriteLine("Philosoph {0} is waiting. Thread [{1}]", this.id, Thread.CurrentThread.ManagedThreadId);
                //    }
                //    Thread.Sleep(1000);
                //}
                //if (runTread == true)
                //{
                //    Console.WriteLine("{0} is done eating", this.name);
                //    Monitor.Exit(this.leftFork.inUse);
                //    Monitor.Exit(this.rightFork.inUse);
                //}
                #endregion
            }
            if (runTread == true)
            {
                Console.WriteLine("Philosopher {0} is done eating", this.id);
            }
        }

        private void TryOtherFork(Fork currentFork)
        {
            Fork otherFork;
            if (currentFork == this.leftFork)
            {
                otherFork = this.rightFork;
            }
            else
            {
                otherFork = this.leftFork;
            }

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Monitor.TryEnter(otherFork.inUse))
                    {
                        try
                        {
                            Eat();
                        }
                        finally
                        {
                            SurrenderFork(otherFork);
                        }
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
            finally
            {
                SurrenderFork(currentFork);
            }
        }

        private void SurrenderFork(Fork currentFork)
        {
            Console.WriteLine("Philosopher {0} is surrenders fork {1}"
                            , this.id
                            , currentFork.id);
            Monitor.Exit(currentFork.inUse);
        }

        private void Eat()
        {
            this.spaghetti -= 100;
            Console.WriteLine("Philosoph {0} is eating. Spaghetti left {4}"
                , this.id
                , this.rightFork.id
                , this.leftFork.id
                , Thread.CurrentThread.ManagedThreadId
                , this.spaghetti);
            Thread.Sleep(1000);
        }
    }
}
