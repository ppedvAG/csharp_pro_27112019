using System;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 10000000, i => Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}"));

            var t1 = new Task(() =>
            {
                Console.WriteLine("t1 gestartet");
                Thread.Sleep(800);
                Console.WriteLine("t1 fertig");
            });

            t1.Start();

            Console.WriteLine("Ende");
            Console.ReadKey();
        }

        private static void Zähle()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {i}");
            }
        }
    }
}
