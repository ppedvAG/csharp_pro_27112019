using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ppedv.Kursverwaltung.Model;

namespace ppedv.Kursverwaltung.Data.EF.Tests
{
    [TestClass]
    public class EfContextTests
    {
        [TestMethod]
        public void Can_create_DB()
        {
            using (var con = new EfContext())
            {
                if (con.Database.Exists())
                    con.Database.Delete();

                con.Database.Create();

                Assert.IsTrue(con.Database.Exists());
            }
        }

        [TestMethod]
        public void Can_CRUD_Kurs()
        {
            var kurs = new Kurs()
            {
                Start = new DateTime(2000, 1, 1),
                Ort = $"Düsseldoof_{Guid.NewGuid()}"
            };
            string newOrt = $"Köln_{Guid.NewGuid()}";

            //INSERT
            using (var con = new EfContext())
            {
                con.Kurse.Add(kurs);
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //READ
                var loaded = con.Kurse.Find(kurs.Id);
                Assert.IsNotNull(loaded);
                Assert.AreEqual(kurs.Ort, loaded.Ort);

                //UPDATE
                loaded.Ort = newOrt;
                con.SaveChanges();
            }

            using (var con = new EfContext())
            {
                //Test UPDATE
                var loaded = con.Kurse.Find(kurs.Id);
                Assert.AreEqual(newOrt, loaded.Ort);

                //DELETE
                con.Kurse.Remove(loaded);
                con.SaveChanges();
            }

            //test DELETE
            using (var con = new EfContext())
            {
                //READ
                var loaded = con.Kurse.Find(kurs.Id);
                Assert.IsNull(loaded);
            }
        }
    }
}
