using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppedv.Kursverwaltung.UI.Web.Models
{
    public class KursViewModel
    {
        public int Id { get; set; }
        public string Ort { get; set; }
        public string Thema { get; set; }
        public string Trainer { get; set; }
        public DateTime Start { get; set; }
    }
}
