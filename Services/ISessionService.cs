namespace PaintTintingDesktopApp.Services
{
    public interface ISessionService<TTarget>
    {
        void Abandon();
        TTarget PermanentIdentity { get; set; }
        TTarget TemporaryIdentity { get; set; }
    }
}
