using PaintTintingDesktopApp.Services;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Strategies
{
    public interface IPrintStrategy
    {
        Task PrintAsync(object parameter, IMessageBoxService notifier);
    }
}
