﻿using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Models.PartialModels;
using PaintTintingDesktopApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace PaintTintingDesktopApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigationService<ViewModelBase> NavigationService =>
            DependencyService.Get<INavigationService<ViewModelBase>>();
        public IDataStore<LoginUser> LoginDataStore =>
            DependencyService.Get<IDataStore<LoginUser>>();
        public IDataStore<Paint> PaintDataStore =>
            DependencyService.Get<IDataStore<Paint>>();
        public IDataStore<RegisterUser> RegisterDataStore =>
            DependencyService.Get<IDataStore<RegisterUser>>();
        public IDataStore<User> UserDataStore =>
            DependencyService.Get<IDataStore<User>>();
        public IDataStore<Service> ServicesDataStore =>
            DependencyService.Get<IDataStore<Service>>();
        public IDataStore<Contact> ContactsDataStore =>
            DependencyService.Get<IDataStore<Contact>>();
        public IMessageBoxService MessageBoxService =>
            DependencyService.Get<IMessageBoxService>();
        public ISessionService<User> SessionService =>
            DependencyService.Get<ISessionService<User>>();
        public IBlenderService BlenderService =>
            DependencyService.Get<IBlenderService>();
        public IClosestColorService ClosestColorService =>
            DependencyService.Get<IClosestColorService>();

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                {
                    OnPropertyChanged(
                        nameof(IsNotBusy));
                }
            }
        }
        public bool IsNotBusy => !IsBusy;
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private string title = string.Empty;

        public ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new DependencyObject()))
            {
                _ = typeof(App)
                    .GetMethod("OnStartup")
                    .Invoke(
                        new App(), null);
            }
        }

        public string Title
        {
            get => title;
            set => _ = SetProperty(ref title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
