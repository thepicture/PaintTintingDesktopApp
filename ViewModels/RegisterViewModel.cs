using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.PartialModels;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private RegisterUser user = new RegisterUser();

        public RegisterViewModel()
        {
            Title = "Страница регистрации";
        }

        public RegisterUser User
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

        private async void RegisterAsync()
        {
            if (await RegisterDataStore.AddItemAsync(User))
            {
                NavigationService.NavigateBack();
            }
        }
    }
}