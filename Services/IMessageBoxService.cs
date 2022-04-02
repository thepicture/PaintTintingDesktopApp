using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public interface IMessageBoxService
    {
        Task InformAsync(string information);
        Task WarnAsync(string warning);
        Task<bool> AskAsync(string question);
        Task InformErrorAsync(string error);
    }
}
