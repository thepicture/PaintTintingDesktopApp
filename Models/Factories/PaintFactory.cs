using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Models.Factories
{
    public static class PaintFactory
    {
        public static Paint CreatePaint(string hexColor)
        {
            return new Paint
            {
                ColorAsHex = "#" + hexColor.ToUpper()
            };
        }

        public static Paint CreatePaint(Color selectedColor)
        {
            return new Paint
            {
                ColorAsHex = "#" + selectedColor.R.ToString("x2") +
                    selectedColor.G.ToString("x2") +
                    selectedColor.B.ToString("x2")
            };
        }
    }

}
