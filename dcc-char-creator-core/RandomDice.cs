using System;
using System.Linq;

namespace DccCharCreator.core
{
    class RandomDice
    {
        private static Random random = new Random();

        public static int D6(int count = 1)
        {
            if(count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return Enumerable.Range(1, count).Sum(x => random.Next(1, 7));
        }

        public static int D12(int count = 1)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return Enumerable.Range(1, count).Sum(x => random.Next(1, 13));
        }

        internal static int D4()
        {
            return random.Next(1, 5);
        }

        internal static int D30()
        {
            return random.Next(1, 31);
        }

        public static int D100()
        {
            return random.Next(1, 101);
        }
    }
}
