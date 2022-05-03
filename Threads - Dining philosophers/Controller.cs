using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads___Dining_philosophers
{
    internal class Controller
    {
        public Philosoph[] CreatePhilosophers(Fork[] forks)
        {
            Philosoph[] philosophs = new Philosoph[5];
            string[] philNames = new string[5] { "John", "Jane", "Robert", "Garry", "Emma" };

            for (int i = 0; i < forks.Length; i++)
            {
                if (i + 1 == forks.Length)
                {
                    philosophs[i] = new Philosoph(forks[i], forks[0], i, philNames[i]);
                }
                else
                {
                    philosophs[i] = new Philosoph(forks[i], forks[i + 1], i, philNames[i]);
                }
            }

            return philosophs;
        }

        public Fork[] CreateForks()
        {
            Fork[] forks = new Fork[5];
            for (int i = 0; i < 5; i++)
            {
                forks[i] = new Fork(i);
            }

            return forks;
        }

        public void GetPhilInfo(Philosoph[] philosophs)
        {
            foreach (Philosoph phil in philosophs)
            {
                Console.WriteLine("{0} has forks {1} and {2}"
                    , phil.name
                    , phil.rightFork.id
                    , phil.leftFork.id);
            }
        }


    }
}
