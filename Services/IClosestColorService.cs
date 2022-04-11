using System.Collections.Generic;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Services
{
    public interface IClosestColorService
    {
        Color? GetClosestColor(IEnumerable<Color> colorArray,
                                        Color baseColor);

        int GetDiff(Color color, Color baseColor);
    }
}
