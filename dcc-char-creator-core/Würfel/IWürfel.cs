namespace DccCharCreator.core.Würfel
{
    public interface IWürfel
    {
        int Würfeln();
    }

    public interface IW4 : IWürfel { }
    public interface I3W6 : IWürfel { }
    public interface IW24 : IWürfel { }
    public interface IW30 : IWürfel { }
    public interface IW100 : IWürfel { }
    public interface I4W20 : IWürfel { }
    public interface IW27 : IWürfel { }
    public interface I5W12 : IWürfel { }
    public interface IW6 : IWürfel { }
    public interface IW8 : IWürfel { }
    public interface IW10 : IWürfel { }

}
