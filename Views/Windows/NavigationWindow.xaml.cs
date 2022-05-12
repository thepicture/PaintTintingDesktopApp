using PaintTintingDesktopApp.Properties;
using PaintTintingDesktopApp.ViewModels;
using System.Windows;

namespace PaintTintingDesktopApp
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        public NavigationWindow()
        {
            InitializeComponent();
            DataContext = new NavigationViewModel();
        }

        private void OnLanguagesLoaded(object sender, RoutedEventArgs e)
        {
            ComboLanguages.SelectedIndex = Settings.Default.CurrentLanguage == "en"
                ? 0
                : 1;
        }
    }
}
