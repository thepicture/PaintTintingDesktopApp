using System.Collections.Generic;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Services
{
    public class BlenderService : IBlenderService
    {
        public ICollection<Color> GetTriadicColors(
            Color color)
        {
            string hexadecimalColor = color
                .ToString()
                .Substring(3);
            string r = hexadecimalColor.Substring(0, 2);
            string g = hexadecimalColor.Substring(2, 2);
            string b = hexadecimalColor.Substring(4, 2);
            Color triad1 = (Color)ColorConverter.ConvertFromString("#" + b + r + g);
            Color triad2 = (Color)ColorConverter.ConvertFromString("#" + g + b + r);
            return new List<Color> { triad1, triad2 };
        }

        public Color Mix(Color color1,
                         Color color2)
        {
            byte a = (byte)((color1.A + color2.A) / 2);
            byte r = (byte)((color1.R + color2.R) / 2);
            byte g = (byte)((color1.G + color2.G) / 2);
            byte b = (byte)((color1.B + color2.B) / 2);
            return Color.FromRgb(r, g, b);
        }
    }
}
