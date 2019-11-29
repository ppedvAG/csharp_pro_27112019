using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Thema : Entity
    {
        public string Beschreibung { get; set; }
        public virtual ICollection<Kurs> Kurse { get; set; } = new HashSet<Kurs>();
        public virtual ICollection<Trainer> Trainer { get; set; } = new HashSet<Trainer>();
    }
}
