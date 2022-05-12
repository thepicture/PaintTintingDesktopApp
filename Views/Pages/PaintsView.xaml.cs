using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaintTintingDesktopApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for PaintsView.xaml
    /// </summary>
    public partial class PaintsView : UserControl
    {
        public PaintsView()
        {
            InitializeComponent();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            (sender as Flipper).IsFlipped = !(sender as Flipper).IsFlipped;
        }

        private void OnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            ((dynamic)DataContext).LoadPaintsAsync();
        }
    }
}
