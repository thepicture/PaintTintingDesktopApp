using PaintTintingDesktopApp.Commands;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        public ControlPanelViewModel()
        {
            Title = "Панель управления";
        }

        private Command goToPaintTintingBuildViewModelCommand;

        public ICommand GoToPaintTintingBuildViewModelCommand
        {
            get
            {
                if (goToPaintTintingBuildViewModelCommand == null)
                {
                    goToPaintTintingBuildViewModelCommand = new Command(GoToPaintTintingBuildViewModel);
                }

                return goToPaintTintingBuildViewModelCommand;
            }
        }

        private void GoToPaintTintingBuildViewModel()
        {
            NavigationService.Navigate<PaintTintingBuildViewModel>();
        }

        private Command goToAccountViewModelCommand;

        public ICommand GoToAccountViewModelCommand
        {
            get
            {
                if (goToAccountViewModelCommand == null)
                {
                    goToAccountViewModelCommand = new Command(GoToAccountViewModel);
                }

                return goToAccountViewModelCommand;
            }
        }

        private void GoToAccountViewModel()
        {
            NavigationService.Navigate<AccountViewModel>();
        }
    }
}