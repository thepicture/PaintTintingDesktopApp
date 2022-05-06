using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Services.Tests
{
    [TestClass()]
    public class BlenderServiceTests
    {
        private readonly IBlenderService blenderService = new BlenderService();

        [TestMethod()]
        public void Mix_MixFF0000And0000FF_Returns800080IsTheSameWith7F007F()
        {
            #region arrange
            Color expected = Color.FromRgb(0x7f, 0, 0x7f);
            #endregion
            #region act
            Color color1 = Color.FromRgb(255, 0, 0);
            Color color2 = Color.FromRgb(0, 0, 255);
            Color actual = blenderService.Mix(color1, color2);
            #endregion
            #region assert
            Assert.AreEqual(expected, actual);
            #endregion
        }

        [TestMethod()]
        public void GetTriadicColors_InputIsAA00FF_ReturnsFFAA00And00FFAA()
        {
            #region arrange
            IEnumerable<Color> expected = new List<Color>
            {
                Color.FromRgb(0xff,0xaa,0x00),
                Color.FromRgb(0x00,0xff,0xaa)
            };
            #endregion
            #region act
            Color color = Color.FromRgb(0xaa, 0x00, 0xff);
            ICollection<Color> actual = blenderService
                    .GetTriadicColors(color);
            #endregion
            #region assert
            Assert.AreEqual(true, Enumerable.SequenceEqual(expected, actual));
            #endregion
        }
    }
}