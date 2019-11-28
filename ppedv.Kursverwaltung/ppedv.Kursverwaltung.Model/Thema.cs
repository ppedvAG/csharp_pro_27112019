using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Thema : Entity
    {
        public string Beschreibung { get; set; }
        public ICollection<Kurs> Kurse { get; set; } = new HashSet<Kurs>();
        public ICollection<Trainer> Trainer { get; set; } = new HashSet<Trainer>();
    }
}
