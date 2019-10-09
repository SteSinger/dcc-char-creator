using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DccCharCreator.core
{
    public struct Attribut : IEquatable<Attribut>
    {
        public int value;

        public Attribut(int value) : this()
        {
            this.value = value;
        }

        public int Bonus =>
            value switch
            {
                3 => -3,
                4 => -2,
                5 => -2,
                6 => -1,
                7 => -1,
                8 => -1,
                9 => 0,
                10 => 0,
                11 => 0,
                12 => 0,
                13 => 1,
                14 => 1,
                15 => 1,
                16 => 2,
                17 => 2,
                18 => 3,
                _ => throw new Exception("Attributswert muss zwischen 3 und 18 liegen"),
            };

        public string BonusFormatted => Bonus.ToString("+0;-0;0", CultureInfo.InvariantCulture);

        public override bool Equals(object? obj)
        {
            if(obj is null)
            {
                return false;
            }

            return ((Attribut)obj).value == value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static bool operator ==(Attribut left, Attribut right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Attribut left, Attribut right)
        {
            return !(left == right);
        }

        public bool Equals(Attribut other)
        {
            return other.value == value;
        }
    }
}
