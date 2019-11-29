using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Trainer : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Thema> Themen { get; set; } = new HashSet<Thema>();
        public virtual ICollection<Kurs> Kurse { get; set; } = new HashSet<Kurs>();
    }
}
