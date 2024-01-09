using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DiningPhilosophers
{
    internal class Program
    {
        private const int NumPhilosophers = 5;
        private static readonly Philosopher[] Philosophers = new Philosopher[NumPhilosophers];
        private static readonly Semaphore[] Forks = new Semaphore[NumPhilosophers];
        private static readonly Semaphore DiningTableSemaphore = new Semaphore(NumPhilosophers / 2, NumPhilosophers / 2);

        private static void Main()
        {
            for (int i = 0; i < NumPhilosophers; i++)
            {
                Forks[i] = new Semaphore(1, 1);
            }

            for (int i = 0; i < NumPhilosophers; i++)
            {
                Philosophers[i] = new Philosopher(DiningTableSemaphore, Forks[i], Forks[(i+1)%NumPhilosophers]);
                Philosophers[i].StartDinner();
            }

            while (true)
            {
                string eatDebug = "eating:";
                string thinkDebug = " thinking:";
                bool continueDinner = false;
                
                for (int i = 0; i < NumPhilosophers; i++)
                {
                    if (Philosophers[i].IsEating)
                    {
                        eatDebug += $" {i}({i},{(i + 1) % NumPhilosophers})";
                    }
                    else
                    {
                        thinkDebug += $" {i}";
                    }
                    if (Philosophers[i].EatCount == 0)
                    {
                        continueDinner = true;
                    }
                }
                Console.WriteLine(eatDebug + thinkDebug);
                if (!continueDinner)
                {
                    break;
                }
                Thread.Sleep(300);
            }
            for (int i = 0; i < NumPhilosophers; i++)
            {
                Philosophers[i].StopDinner();
            }
            for (int i = 0; i < NumPhilosophers; i++)
            {
                Philosophers[i].WaitEnd();
            }
        }
    }
}