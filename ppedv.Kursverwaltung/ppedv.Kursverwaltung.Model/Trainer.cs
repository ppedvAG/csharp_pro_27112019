using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Trainer : Entity
    {
        public string Name { get; set; }
        public ICollection<Thema> Themen { get; set; } = new HashSet<Thema>();
        public ICollection<Kurs> Kurse { get; set; } = new HashSet<Kurs>();
    }
}
