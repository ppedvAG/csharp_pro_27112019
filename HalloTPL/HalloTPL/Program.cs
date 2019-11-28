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
                throw new NullReferenceException();
            });

            t1.ContinueWith(t => Console.WriteLine("T1c fertig"), TaskContinuationOptions.None);
            t1.ContinueWith(t => Console.WriteLine($"T1 ERROR {t.Exception.InnerException.Message}"), TaskContinuationOptions.OnlyOnFaulted);
            t1.ContinueWith(t => Console.WriteLine("T1 OK"), TaskContinuationOptions.OnlyOnRanToCompletion);

            var t2 = new Task<long>(() =>
            {
                Console.WriteLine("t2 gestartet");
                Thread.Sleep(600);
                Console.WriteLine("t2 fertig");
                return 34567890;
            });

            t2.ContinueWith(t =>
            {
                Console.WriteLine($"T2 Continue {t.Result}");
            });

            t1.Start();
            t2.Start();

            //t2.Wait();
            //Task.WaitAll(t1, t2);
            //Console.WriteLine($"{t2.Result}");

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
