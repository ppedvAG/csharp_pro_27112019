using Bogus;
using ppedv.Kursverwaltung.Model;
using ppedv.Kursverwaltung.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ppedv.Kursverwaltung.Logic
{
    public class Core
    {
        public IRepository Repository { get; }

        public Core(IRepository repo)
        {
            this.Repository = repo;
        }

        public Core() : this(new Data.EF.EfRepository())
        { }

        public void CreateDemodaten()
        {
            var faker = new Faker(locale: "de");

            List<Trainer> trainers = new List<Trainer>();
            for (int i = 0; i < 5; i++)
            {
                trainers.Add(new Trainer() { Name = faker.Name.FullName() });
            }

            for (int i = 0; i < 10; i++)
            {
                var t = new Thema()
                {
                    Beschreibung = faker.Vehicle.Manufacturer(),
                    Trainer = faker.Random.ListItems(trainers)
                };

                for (int j = 0; j < faker.Random.Int(10, 100); j++)
                {
                    var k = new Kurs()
                    {
                        Trainer = faker.PickRandom(trainers),
                        Thema = t,
                        Ort = faker.Address.City(),
                        Start = faker.Date.RecentOffset(1000).Date
                    };
                    Repository.Add(k);
                }
            }
            Repository.Save();
        }
    }
}
