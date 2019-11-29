using ppedv.Kursverwaltung.Logic;
using ppedv.Kursverwaltung.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.Kursverwaltung.UI.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("*** Kursverwaltung v0.1 ***");

            var core = new Core();

            if (core.Repository.GetAll<Trainer>().Count() == 0)
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
