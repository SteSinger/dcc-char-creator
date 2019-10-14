namespace DccCharCreator.core.Würfel
{
    public static class WürfelFactory
    {
        public static IW4 W4 => new WürfelBecher(() => WürfelFunktionen.W4());

        public static I3W6 _3W6 => new WürfelBecher(() => WürfelFunktionen.W6(3));

        public static IW100 W100 => new WürfelBecher(() => WürfelFunktionen.W100());

        public static IW100 WProzent => W100;

        public static IW30 W30 => new WürfelBecher(() => WürfelFunktionen.W30());

        public static I5W12 _5W12 => new WürfelBecher(() => WürfelFunktionen.W12(5));

        public static IW24 W24 => new WürfelBecher(() => WürfelFunktionen.W24());
    }
}
