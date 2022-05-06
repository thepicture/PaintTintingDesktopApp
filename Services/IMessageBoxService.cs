using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public interface IMessageBoxService
    {
        Task InformAsync(object information);
        Task WarnAsync(object warning);
        Task<bool> AskAsync(object question);
        Task InformErrorAsync(object error);
    }
}
