using System;
using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Kurs : Entity
    {
        public Thema Thema { get; set; }
        public string Ort { get; set; }
        public DateTime Start { get; set; } = DateTime.Now;
        public ICollection<Teilnehmer> Teilnehmer { get; set; } = new HashSet<Teilnehmer>();
        public Trainer Trainer { get; set; }
    }
}
