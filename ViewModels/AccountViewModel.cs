using CodingSeb.Localization;
using Microsoft.Win32;
using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using System.IO;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AccountViewModel : ViewModelBase
    {
        public AccountViewModel()
        {
            Title = Loc.Tr(
                GetType().Name);
        }

        private Command selectImageCommand;

        public ICommand SelectImageCommand
        {
            get
            {
                if (selectImageCommand == null)
                {
                    selectImageCommand = new Command(SelectImageAsync);
                }

                return selectImageCommand;
            }
        }

        private async void SelectImageAsync()
        {
            OpenFileDialog imageFileDialog = new OpenFileDialog
            {
                Title = "Выберите новый аватар"
            };
            bool? isSelectedImage = imageFileDialog.ShowDialog();
            if (isSelectedImage.HasValue && isSelectedImage.Value)
            {
                User sessionUser = SessionService.TemporaryIdentity;
                sessionUser.Photo = File.ReadAllBytes(imageFileDialog.FileName);
                if (await UserDataStore.UpdateItemAsync(sessionUser))
                {
                    if (SessionService.PermanentIdentity != null)
                    {
                        SessionService.PermanentIdentity = sessionUser;
                    }
                    else
                    {
                        SessionService.TemporaryIdentity = sessionUser;
                    }
                }
            }
        }
    }
}