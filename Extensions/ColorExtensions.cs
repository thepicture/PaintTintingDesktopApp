namespace PaintTintingDesktopApp.Extensions
{
    public static class ColorExtensions
    {
        public static float GetHue(this System.Windows.Media.Color c)
        {
            return System.Drawing.Color
                .FromArgb(c.A, c.R, c.G, c.B)
                .GetHue();
        }
    }
}
