using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using PaintTintingDesktopApp.Services;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private User user;

        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }
        public IPasswordHashService PasswordHashService =>
            DependencyService.Get<IPasswordHashService>();
        public LoginViewModel()
        {
            Title = "Страница авторизации";
            User = new User();
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

        private async void LoginAsync(object parameter)
        {
            if (await LoginDataStore.AddItemAsync(User))
            {
                if (IsRememberMe)
                {
                    Settings.Default.IsAuthenticated = true;
                    Settings.Default.Save();
                }
            }
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

        private async void ExitAsync(object parameter)
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

        private void GoToRegisterPageAsync(object commandParameter)
        {
            NavigationService.Navigate<RegisterViewModel>();
        }
    }
}