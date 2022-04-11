using System.Collections.Generic;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Services
{
    public interface IBlenderService
    {
        Color Mix(Color color1,
                  Color color2);
        ICollection<Color> GetTriadicColors(Color color);
    }
}
