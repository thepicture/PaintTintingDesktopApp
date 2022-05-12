using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Models.PartialModels;
using PaintTintingDesktopApp.Services;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginUser user = new LoginUser();

        public LoginUser User
        {
            get => user;
            set => SetProperty(ref user, value);
        }
        public IPasswordHashService PasswordHashService =>
            DependencyService.Get<IPasswordHashService>();
        public LoginViewModel()
        {
            Title = "Страница авторизации";
        }

        private Command loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new Command(LoginAsync);
                }

                return loginCommand;
            }
        }

        private async void LoginAsync()
        {
            IsBusy = true;
            if (await LoginDataStore.AddItemAsync(User))
            {
                if (await LoginDataStore.GetItemAsync(User.Login) != null)
                {
                    User identity = await LoginDataStore
                        .GetItemAsync(User.Login);
                    if (IsRememberMe)
                    {
                        SessionService.PermanentIdentity = identity;
                    }
                    else
                    {
                        SessionService.TemporaryIdentity = identity;
                    }
                }
                NavigationService.Navigate<ControlPanelViewModel>();
            }
            IsBusy = false;
        }

        private Command exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new Command(ExitAsync);
                }

                return exitCommand;
            }
        }

        private async void ExitAsync()
        {
            if (await MessageBoxService.AskAsync("Выйти из приложения?"))
            {
                App.Current.Shutdown();
            }
        }

        private bool isRememberMe = false;

        public bool IsRememberMe
        {
            get => isRememberMe;
            set => SetProperty(ref isRememberMe, value);
        }

        private Command goToRegisterPageCommand;

        public ICommand GoToRegisterPageCommand
        {
            get
            {
                if (goToRegisterPageCommand == null)
                {
                    goToRegisterPageCommand = new Command(GoToRegisterPageAsync);
                }

                return goToRegisterPageCommand;
            }
        }

        private void GoToRegisterPageAsync()
        {
            NavigationService.Navigate<RegisterViewModel>();
        }
    }
}