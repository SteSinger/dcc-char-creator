﻿using System;

namespace DccCharCreator.core.Würfel
{
    public class WürfelFactory : IWürfelFactory
    {
        private readonly WürfelFunktionen würfel;

        public WürfelFactory(Random random)
        {
            würfel = new WürfelFunktionen(random);
        }


        public IW4 W4 => new WürfelBecher(() => würfel.W(4));
        
        public IW5 W5 => new WürfelBecher(() => würfel.W(5));

        public I3W6 _3W6 => new WürfelBecher(() => würfel.W(6, 3));

        public IW100 W100 => new WürfelBecher(() => würfel.W(100));

        public IW100 WProzent => W100;

        public IW30 W30 => new WürfelBecher(() => würfel.W(30));

        public I4W20 _4W20 => new WürfelBecher(() => würfel.W(20, 4));

        public IW27 W27 => new WürfelBecher(() => würfel.W(27));

        public I5W12 _5W12 => new WürfelBecher(() => würfel.W(12, 5));

        public IW24 W24 => new WürfelBecher(() => würfel.W(24));

        public IW6 W6 => new WürfelBecher(() => würfel.W(6));

        public IW8 W8 => new WürfelBecher(() => würfel.W(8));

        public IW10 W10 => new WürfelBecher(() => würfel.W(10));

        public IW11 W11 => new WürfelBecher(() => würfel.W(11));

        public IW3 W3 => new WürfelBecher(() => würfel.W(3));
    }
}
