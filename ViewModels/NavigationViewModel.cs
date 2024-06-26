﻿using CodingSeb.Localization;
using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Properties;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            NavigationService.Navigated += OnNavigated;
        }

        public ObservableCollection<string> Languages { get; set; }

        private ComboBoxItem currentLanguage;

        public ComboBoxItem CurrentLanguage
        {
            get => currentLanguage;

            set
            {
                if (SetProperty(ref currentLanguage, value))
                {
                    if (currentLanguage.Content.ToString() == "English"
                        || currentLanguage.Content.ToString() == "Английский")
                    {
                        Settings.Default.CurrentLanguage = "en";
                    }
                    else
                    {
                        Settings.Default.CurrentLanguage = "ru";
                    }
                    Settings.Default.Save();
                    Loc.Instance.CurrentLanguage = Settings.Default.CurrentLanguage;
                    CurrentViewModel.Title = Loc.Tr(
                        CurrentViewModel.GetType().Name);
                }
            }
        }

        private void OnNavigated()
        {
            OnPropertyChanged(
                nameof(CurrentViewModel));
            OnPropertyChanged(
                nameof(CanGoBack));
            OnPropertyChanged(
                nameof(CanClearSession));
        }

        public ViewModelBase CurrentViewModel =>
            NavigationService.CurrentTarget;
        public bool CanGoBack =>
            NavigationService.CanNavigateBack();
        public bool CanClearSession
        {
            get
            {
                return SessionService.PermanentIdentity != null
                       || SessionService.TemporaryIdentity != null;
            }
        }

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

        private Command clearSessionCommand;

        public ICommand ClearSessionCommand
        {
            get
            {
                if (clearSessionCommand == null)
                {
                    clearSessionCommand = new Command(ClearSessionAsync);
                }

                return clearSessionCommand;
            }
        }

        private async void ClearSessionAsync()
        {
            if (await MessageBoxService.AskAsync("Завершить сессию "
                    + "и вернуться на страницу авторизации?"))
            {
                SessionService.Abandon();
                NavigationService.NavigateToRoot();
            }
        }
    }
}