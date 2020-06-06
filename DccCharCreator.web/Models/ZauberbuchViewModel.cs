using DccCharCreator.core.Zauberbuch;
using System.Collections.Generic;

namespace DccCharCreator.web.Models
{
    public class ZauberbuchViewModel
    {
        public int Glueck { get; set; }
        public int Intelligenz { get; set; }
        public int Stufe { get; set; }
        public IList<Zauber> Zauberbuch { get; set; }
        public Klasse Klasse { get; set; }
    }
}
