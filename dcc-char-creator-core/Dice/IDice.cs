using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.Dice
{
    public interface IDice
    {
        int Roll();
    }

    public interface ID4 : IDice { }
    public interface I3D6 : IDice { }
    public interface ID30 : IDice { }
    public interface ID100 : IDice { }
    public interface I5D12 : IDice { }
}
