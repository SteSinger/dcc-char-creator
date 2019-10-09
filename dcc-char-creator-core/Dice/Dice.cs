using DccCharCreator.core;
using System;

namespace DccCharCreator.core.Dice
{
    internal class Dice : ID4, I3D6, ID30, ID100, I5D12
    {
        public Dice(Func<int> dice)
        {
            diceRoll = dice;
        }

        public Func<int> diceRoll { get; }

        public int Roll()
        {
            return diceRoll();
        }
    }
}
