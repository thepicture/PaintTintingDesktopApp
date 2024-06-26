﻿using PaintTintingDesktopApp.Commands;
using System.Windows.Input;

namespace PaintTintingDesktopApp.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        public ControlPanelViewModel()
        {
            Title = CodingSeb.Localization.Loc.Tr(
                GetType().Name);
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

        private Command goToServicesViewModel;

        public ICommand GoToServicesViewModel
        {
            get
            {
                if (goToServicesViewModel == null)
                {
                    goToServicesViewModel = new Command(PerformGoToServicesViewModel);
                }

                return goToServicesViewModel;
            }
        }

        private void PerformGoToServicesViewModel()
        {
            NavigationService.Navigate<ServicesViewModel>();
        }

        private Command goToContactsViewModel;

        public ICommand GoToContactsViewModel
        {
            get
            {
                if (goToContactsViewModel == null)
                {
                    goToContactsViewModel = new Command(PerformGoToContactsViewModel);
                }

                return goToContactsViewModel;
            }
        }

        private void PerformGoToContactsViewModel()
        {
            NavigationService.Navigate<ContactsViewModel>();
        }

        private Command goToPaintsViewModel;

        public ICommand GoToPaintsViewModel
        {
            get
            {
                if (goToPaintsViewModel == null)
                {
                    goToPaintsViewModel = new Command(PerformGoToPaintsViewModel);
                }

                return goToPaintsViewModel;
            }
        }

        private void PerformGoToPaintsViewModel()
        {
            NavigationService.Navigate<PaintsViewModel>();
        }
    }
}