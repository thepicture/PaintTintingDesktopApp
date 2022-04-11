using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private Color selectedColor = Color.FromRgb(255, 0, 0);

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
            ICollection<Color> triadicColors = BlenderService
                .GetTriadicColors(SelectedColor);
            FirstTriadicColor = triadicColors.First();
            SecondTriadicColor = triadicColors.Last();
            await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    string hex = ColorAsHex.ToString();
                }
            });
        }

        private Color firstTriadicColor;

        public Color FirstTriadicColor
        {
            get => firstTriadicColor;
            set => SetProperty(ref firstTriadicColor, value);
        }

        private Color secondTriadicColor;

        public Color SecondTriadicColor
        {
            get => secondTriadicColor;
            set => SetProperty(ref secondTriadicColor, value);
        }

        private Command mixColorsCommand;

        public ICommand MixColorsCommand
        {
            get
            {
                if (mixColorsCommand == null)
                {
                    mixColorsCommand = new Command(MixColorsAsync);
                }

                return mixColorsCommand;
            }
        }

        private async void MixColorsAsync()
        {
        }
    }
}