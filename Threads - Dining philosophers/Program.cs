using System;
using System.Threading;
using Threads___Dining_philosophers;


class Program
{
    static bool keepRunning = true;
    //public static Fork[] forks = new Fork[5] 
    //{ 
    //    new Fork(0), 
    //    new Fork(1), 
    //    new Fork(2), 
    //    new Fork(3), 
    //    new Fork(4) 
    //};
    static void Main()
    {
        Controller controller = new Controller();


        Fork[] forks = controller.CreateForks();
        Philosoph[] philosophs = controller.CreatePhilosophers(forks);

        Console.WriteLine("1. Get table layout");
        Console.WriteLine("2. Begin eating");

        switch (Console.ReadKey(true).KeyChar)
        {
            case '1':
                controller.GetPhilInfo(philosophs);
                break;
            case '2':
                StartEating(philosophs);
                break;
        }
        while(keepRunning)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                keepRunning = false;
                foreach (Philosoph phil in philosophs)
                {
                    phil.runTread = false;

                }
            }
        }
        Console.Read();

    }

    static void StartEating(Philosoph[] philosophs)
    {
        foreach (Philosoph phil in philosophs)
        {
            phil.thread.Start();
            //Thread thread = new Thread(phil.EatCheck);
            //thread.Name = "Philosopher " + phil.id;
            //thread.Start(); 

        }
    }
}