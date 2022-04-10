using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private User user;

        public RegisterViewModel()
        {
            Title = "Страница регистрации";
        }

        public User User
        {
            get => user;
            set => SetProperty(ref user, value);
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private Command registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                {
                    registerCommand = new Command(RegisterAsync);
                }

                return registerCommand;
            }
        }

        private async void RegisterAsync(object commandParameter)
        {
        }
    }
}