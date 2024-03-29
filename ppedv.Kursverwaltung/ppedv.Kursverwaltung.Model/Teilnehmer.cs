﻿using System.Collections.Generic;

namespace ppedv.Kursverwaltung.Model
{
    public class Teilnehmer : Entity
    {
        public string Name { get; set; }
        public virtual ICollection<Kurs> Kurse { get; set; } = new HashSet<Kurs>();
    }
}
