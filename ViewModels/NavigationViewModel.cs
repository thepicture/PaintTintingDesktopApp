using PaintTintingDesktopApp.Services;

namespace PaintTintingDesktopApp.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel => DependencyService
            .Get<INavigationService<ViewModelBase>>()
            .CurrentTarget;
    }
}