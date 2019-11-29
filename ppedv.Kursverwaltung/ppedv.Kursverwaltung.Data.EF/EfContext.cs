using ppedv.Kursverwaltung.Model;
using System.Data.Entity;

namespace ppedv.Kursverwaltung.Data.EF
{
    public class EfContext : DbContext
    {
        public DbSet<Kurs> Kurse { get; set; }
        public DbSet<Teilnehmer> Teilnehmer { get; set; }
        public DbSet<Thema> Themen { get; set; }
        public DbSet<Trainer> Trainer { get; set; }

        public EfContext(string conString) : base(conString)
        { }
        public EfContext() : this("Server=.\\SQLEXPRESS;Database=Kursverwaltung_dev;Trusted_Connection=true;")
        { }
    }
}
