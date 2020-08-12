namespace DccCharCreator.core.Würfel
{
    public interface IWürfelFactory
    {
        IW4 W4 { get; }
        IW5 W5 { get; }
        I3W6 _3W6 { get; }
        IW100 W100 { get; }
        IW100 WProzent { get; }
        IW30 W30 { get; }
        I4W20 _4W20 { get; }
        IW27 W27 { get; }
        I5W12 _5W12 { get; }
        IW24 W24 { get; }
        IW6 W6 { get; }
        IW8 W8 { get; }
        IW10 W10 { get; }
        IW11 W11 { get; }
        IW3 W3 { get; }
    }
}