using System;

namespace DccCharCreator.core.Würfel
{
    internal class WürfelBecher : IW4, I3W6, IW30, IW100, I5W12, IW24, I4W20, IW27
    {
        public WürfelBecher(Func<int> wurf)
        {
            Wurf = wurf;
        }

        private Func<int> Wurf { get; }

        public int Würfeln()
        {
            return Wurf();
        }
    }
}
