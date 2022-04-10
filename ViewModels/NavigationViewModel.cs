using PaintTintingDesktopApp.Commands;
using System.Windows.Input;

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
            OnPropertyChanged(
                nameof(CanGoBack));
        }

        public ViewModelBase CurrentViewModel =>
            NavigationService.CurrentTarget;
        public bool CanGoBack =>
            NavigationService.CanNavigateBack();

        private Command goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                if (goBackCommand == null)
                {
                    goBackCommand = new Command(GoBack);
                }

                return goBackCommand;
            }
        }

        private void GoBack(object commandParameter)
        {
            NavigationService.NavigateBack();
        }
    }
}