using PaintTintingDesktopApp.Models.Entities;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PaintTintingDesktopApp.ViewModels
{
    public class PaintTintingBuildViewModel : ViewModelBase
    {
        public PaintTintingBuildViewModel()
        {
            Title = "Страница создания краски";
        }

        public SolidColorBrush ColorAsHex =>
            new SolidColorBrush(SelectedColor);

        private Color selectedColor;

        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (SetProperty(ref selectedColor, value))
                {
                    OnPropertyChanged(
                        nameof(ColorAsHex));
                    SearchTwoPaintingsAsync();
                }
            }
        }

        private async void SearchTwoPaintingsAsync()
        {
            await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    string hex = ColorAsHex.ToString();

                }
            });
        }
    }
}