using ppedv.Kursverwaltung.Logic;
using ppedv.Kursverwaltung.Model;
using ppedv.Kursverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.Kursverwaltung.UI.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Kursverwaltung v0.1 ***");

#if DEBUG
            var path = @"C:\Users\ar2\source\repos\ppedvAG\csharp_pro_27112019\ppedv.Kursverwaltung\ppedv.Kursverwaltung.Data.EF\bin\Debug\ppedv.Kursverwaltung.Data.EF.dll";
#else
            var path = @"C:\Users\ar2\source\repos\ppedvAG\csharp_pro_27112019\ppedv.Kursverwaltung\ppedv.Kursverwaltung.Data.EF\bin\Release\ppedv.Kursverwaltung.Data.EF.dll";
#endif

            var ass = Assembly.LoadFile(path);
            //foreach (var item in ass.GetTypes())
            //{
            //    Console.WriteLine($"{item.FullName}    {string.Join(", ", item.GetTypeInfo().ImplementedInterfaces)}");
            //    foreach (var p in item.GetProperties())
            //        Console.WriteLine($"\t{p.Name}");

            //}

            var derTypMitDemInterface = ass.GetTypes()
                                           .FirstOrDefault(x => x.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IRepository)));

            var repo = Activator.CreateInstance(derTypMitDemInterface) as IRepository;


            var core = new Core(repo);

            //if (core.Repository.GetAll<Trainer>().Count() == 0)
            if (core.Repository.Query<Trainer>().Count() == 0)
                core.CreateDemodaten();

            foreach (var k in core.Repository.GetAll<Kurs>())
            {
                Console.WriteLine($"{k.Start:d} {k.Ort} Thema: {k?.Thema?.Beschreibung} Trainer: {k?.Trainer?.Name}");

            }

            Console.WriteLine("Ende");
            Console.ReadLine();
        }
    }
}
