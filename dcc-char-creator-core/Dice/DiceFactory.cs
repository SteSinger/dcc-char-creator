namespace DccCharCreator.core.Dice
{
    public static class DiceFactory
    {
        public static ID4 W4 => new Dice(() => RandomDice.D4());

        public static I3D6 _3W6 => new Dice(() => RandomDice.D6(3));

        public static ID100 W100 => new Dice(() => RandomDice.D100());

        public static ID100 WPercent => W100;

        public static ID30 W30 => new Dice(() => RandomDice.D30());

        public static I5D12 _5W12 => new Dice(() => RandomDice.D12(5));
    }
}
