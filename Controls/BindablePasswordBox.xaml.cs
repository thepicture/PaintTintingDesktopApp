using System.Windows;
using System.Windows.Controls;

namespace PaintTintingDesktopApp.Controls
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        public string BindablePassword
        {
            get { return (string)GetValue(BindablePasswordProperty); }
            set { SetValue(BindablePasswordProperty, value); }
        }

        public static readonly DependencyProperty BindablePasswordProperty =
            DependencyProperty.Register("BindablePassword", typeof(string), typeof(BindablePasswordBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            BindablePassword = (sender as PasswordBox).Password;
        }
    }
}
