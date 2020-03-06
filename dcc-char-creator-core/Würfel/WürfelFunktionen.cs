using System;
using System.Linq;

namespace DccCharCreator.core.Würfel
{
    internal class WürfelFunktionen
    {
        private readonly Random random;

        internal WürfelFunktionen(Random random)
        {
            this.random = random;
        }

        internal int W(int max, int anzahl = 1)
        {
            if (anzahl <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl));
            }

            return Enumerable.Range(1, anzahl).Sum(x => random.Next(1, max + 1));
        }
    }
}
