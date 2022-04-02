using PaintTintingDesktopApp.Services;
using PaintTintingDesktopApp.ViewModels;
using System.Windows;

namespace PaintTintingDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyService.Register<NavigationService>();
            DependencyService.Get<INavigationService<ViewModelBase>>()
                .Navigate<LoginViewModel>();
        }
    }
}
