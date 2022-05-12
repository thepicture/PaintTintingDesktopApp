using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaintTintingDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for ServicesView.xaml
    /// </summary>
    public partial class ServicesView : UserControl
    {
        public ServicesView()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Flipper).IsFlipped = !(sender as Flipper).IsFlipped;
        }

        private void OnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ((dynamic)DataContext).LoadServicesAsync();
        }
    }
}
