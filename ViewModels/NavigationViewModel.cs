namespace PaintTintingDesktopApp.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            NavigationService.Navigated += OnNavigated;
        }

        private void OnNavigated()
        {
            OnPropertyChanged(
                nameof(CurrentViewModel));
        }

        public ViewModelBase CurrentViewModel => 
            NavigationService.CurrentTarget;
    }
}