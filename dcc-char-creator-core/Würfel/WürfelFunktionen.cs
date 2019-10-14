using System;
using System.Linq;

namespace DccCharCreator.core.Würfel
{
    class WürfelFunktionen
    {
        private static Random random = new Random();

        public static int W6(int anzahl = 1)
        {
            if (anzahl <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl));
            }

            return Enumerable.Range(1, anzahl).Sum(x => random.Next(1, 7));
        }

        public static int W12(int anzahl = 1)
        {
            if (anzahl <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(anzahl));
            }

            return Enumerable.Range(1, anzahl).Sum(x => random.Next(1, 13));
        }

        internal static int W4()
        {
            return random.Next(1, 5);
        }

        internal static int W24()
        {
            return random.Next(1, 25);
        }

        internal static int W30()
        {
            return random.Next(1, 31);
        }

        public static int W100()
        {
            return random.Next(1, 101);
        }
    }
}
