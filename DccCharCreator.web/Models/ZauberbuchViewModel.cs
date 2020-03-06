using DccCharCreator.core.Zauberbuch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DccCharCreator.web.Models
{
    public class ZauberbuchViewModel
    {
        public int Glueck { get; set; }
        public int AnzahlZauber { get; set; }
        public IList<Zauber> Zauberbuch { get; set; }
    }
}
