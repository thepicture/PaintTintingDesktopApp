namespace PaintTintingDesktopApp.Services
{
    public interface IPasswordHashService
    {
        byte[] GetHash(string password, byte[] salt);
    }
}
