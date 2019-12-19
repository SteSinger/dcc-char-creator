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

        internal int W6(int anzahl = 1)
        {
            if (anzahl <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl));
            }

            return Enumerable.Range(1, anzahl).Sum(x => random.Next(1, 7));
        }

        internal int W12(int anzahl = 1)
        {
            if (anzahl <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl));
            }

            return Enumerable.Range(1, anzahl).Sum(x => random.Next(1, 13));
        }

        internal int W4()
        {
            return random.Next(1, 5);
        }

        internal int W24()
        {
            return random.Next(1, 25);
        }

        internal int W30()
        {
            return random.Next(1, 31);
        }

        internal int W100()
        {
            return random.Next(1, 101);
        }
    }
}
