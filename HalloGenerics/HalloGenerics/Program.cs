using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var lt = new LastTwo<int>();
            lt.Add(56);
            lt.Add(-17);
            lt.Add(1084);
            lt.GetAll().ToList().ForEach(x => Console.WriteLine(x));

            foreach (var item in GetZahlen())
            {
                Console.WriteLine(item);
            }

            MachWas<int>(754, 437834);
            MachWas<string>("Hallo", "Welt");
            MachWas<ExecutionEngineException>(new ExecutionEngineException(), null);

            Console.WriteLine("Ende");
            Console.ReadLine();
        }


        static T MachWas<T>(T wert, T wert2)
        {
            Console.WriteLine($"Der typ ist {typeof(T)} mit dem wert: {wert}");
            return wert;
        }

        static IEnumerable<int> GetZahlen()
        {
            yield return 5;
            yield return 1;
            yield return 6;
            yield return -57;
        }

    }
}
