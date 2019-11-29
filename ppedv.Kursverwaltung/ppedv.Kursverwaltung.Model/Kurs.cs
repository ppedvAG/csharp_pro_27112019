using System;
using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Kurs : Entity
    {
        public virtual Thema Thema { get; set; }
        public string Ort { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public virtual ICollection<Teilnehmer> Teilnehmer { get; set; } = new HashSet<Teilnehmer>();
        public virtual Trainer Trainer { get; set; }
    }
}
