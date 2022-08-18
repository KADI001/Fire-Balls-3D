namespace FireBalls3D.Model
{
    public interface IPauseRegister
    {
        public void Register(IPauseable pauseable);
    }

    public interface IPauseUnRegister
    {
        public void UnRegister(IPauseable pauseable);
    }

    public interface IPauseManager : IPauseRegister, IPauseUnRegister, IPauseable
    {
    }
}