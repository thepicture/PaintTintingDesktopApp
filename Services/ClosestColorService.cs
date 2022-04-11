using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Services
{
    public class ClosestColorService : IClosestColorService
    {
        public Color? GetClosestColor(IEnumerable<Color> colorArray, Color baseColor)
        {
            if (colorArray.Count() == 0)
            {
                return null;
            }
            List<(Color Value, int Diff)> colors = colorArray.Select(x =>
            {
                return (Value: x, Diff: GetDiff(x, baseColor));
            })
                .ToList();
            int min = colors.Min(x => x.Diff);
            return colors.Find(x => x.Diff == min).Value;
        }

        public int GetDiff(Color color, Color baseColor)
        {
            int a = color.A - baseColor.A,
                r = color.R - baseColor.R,
                g = color.G - baseColor.G,
                b = color.B - baseColor.B;
            return (a * a) + (r * r) + (g * g) + (b * b);
        }
    }
}
