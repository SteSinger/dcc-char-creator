using System;
using System.Globalization;

namespace DccCharCreator.core.CharacterData
{
    public struct Attribut : IEquatable<Attribut>
    {
        public Attribut(int value) : this()
        {
            Value = value;
        }

        public int Modifikator =>
            Value switch
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

        public string ModifikatorFormattiert => Modifikator.ToString("+0;-0;0", CultureInfo.InvariantCulture);

        public int Value { get; }

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            return ((Attribut)obj).Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
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
            return other.Value == Value;
        }
    }
}
