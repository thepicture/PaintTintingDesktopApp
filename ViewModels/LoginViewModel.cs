using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using PaintTintingDesktopApp.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
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
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(User.Login))
            {
                _ = errors.AppendLine("Введите логин");
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                _ = errors.AppendLine("Введите пароль");
            }
            if (errors.Length > 0)
            {
                await MessageBoxService.InformErrorAsync(
                    errors.ToString());
                return;
            }
            bool isAuthenticated = await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    User dbUser = entities.User
                        .AsNoTracking()
                        .FirstOrDefault(u => u.Login == User.Login);
                    if (dbUser == null)
                    {
                        return false;
                    }
                    byte[] salt = dbUser.Salt;
                    byte[] passwordHash = PasswordHashService
                        .GetHash(Password, salt);
                    return Enumerable.SequenceEqual(dbUser.PasswordHash,
                                                    passwordHash);
                }
            });
            if (isAuthenticated)
            {
                if (IsRememberMe)
                {
                    Settings.Default.IsAuthenticated = true;
                    Settings.Default.Save();
                }
                await MessageBoxService.InformAsync("Вы авторизованы");
            }
            else
            {
                await MessageBoxService
                    .InformAsync("Неверный логин или пароль");
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