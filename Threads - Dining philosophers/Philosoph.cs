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
        Random random = new Random();
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
                if (this.id == 4)
                {
                    if (Monitor.TryEnter(this.rightFork.inUse))
                    {
                        TryOtherFork(this.rightFork);
                    }
                    //else if (Monitor.TryEnter(this.leftFork.inUse))
                    //{
                    //    TryOtherFork(this.leftFork);
                    //}
                    else
                    {
                        Think();
                    }
                }
                else
                {

                    if (Monitor.TryEnter(this.leftFork.inUse))
                    {
                        TryOtherFork(this.leftFork);
                    }
                    //else if (Monitor.TryEnter(this.rightFork.inUse))
                    //{
                    //    TryOtherFork(this.rightFork);
                    //}
                    else
                    {
                        Think();
                    }

                }
                Thread.Sleep(100 / 15);
            }
            if (runTread == true)
            {
                Console.WriteLine("Philosopher {0} is done eating", this.id);
                //Monitor.Exit(this.leftFork.inUse);
                //Monitor.Exit(this.rightFork.inUse);
            }
        }

        private void TryOtherFork(Fork currentFork)
        {
            Fork otherFork;
            if (currentFork.id == this.leftFork.id)
            {
                //Console.WriteLine("Left");
                otherFork = this.rightFork;
            }
            else
            {
                //Console.WriteLine("Right");
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
                        Thread.Sleep(100/15);
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
        private void Think()
        {
            Console.WriteLine("Philosopher number {0} is thinking...", this.id);
            Thread.Sleep(random.Next(100, 1000));
        }

        private void Eat()
        {
            Console.WriteLine("Philosoph {0} is eating. Spaghetti left {1}"
                , this.id
                , this.spaghetti);
            this.spaghetti -= 100;

            Thread.Sleep(random.Next(100, 1000))
                ;
        }
    }
}
