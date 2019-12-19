using System;

namespace DccCharCreator.core.Würfel
{
    public class WürfelFactory
    {
        private readonly WürfelFunktionen würfel;

        public WürfelFactory(Random random)
        {
            würfel = new WürfelFunktionen(random);
        }


        public IW4 W4 => new WürfelBecher(() => würfel.W4());

        public I3W6 _3W6 => new WürfelBecher(() => würfel.W6(3));

        public IW100 W100 => new WürfelBecher(() => würfel.W100());

        public IW100 WProzent => W100;

        public IW30 W30 => new WürfelBecher(() => würfel.W30());

        public I5W12 _5W12 => new WürfelBecher(() => würfel.W12(5));

        public IW24 W24 => new WürfelBecher(() => würfel.W24());
    }
}
