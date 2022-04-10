namespace PaintTintingDesktopApp.Services
{
    public interface ISessionService<TTarget>
    {
        TTarget PermanentIdentity { get; set; }
        TTarget TemporaryIdentity { get; set; }
    }
}
