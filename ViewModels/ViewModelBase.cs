using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PaintTintingDesktopApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigationService<ViewModelBase> NavigationService =>
            DependencyService.Get<INavigationService<ViewModelBase>>();
        public IDataStore<User> LoginDataStore =>
           DependencyService.Get<IDataStore<User>>();
        public IMessageBoxService MessageBoxService =>
            DependencyService.Get<IMessageBoxService>();
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => _ = SetProperty(ref isBusy, value);
        }
        public bool IsNotBusy => !IsBusy;
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private string title = string.Empty;

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
