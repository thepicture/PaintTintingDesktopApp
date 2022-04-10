using PaintTintingDesktopApp.ViewModels;
using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PaintTintingDesktopApp.Services
{
    public class NavigationService : INavigationService<ViewModelBase>
    {
        public NavigationService()
        {
            Journal.CollectionChanged += OnNavigated;
        }

        private void OnNavigated(object sender,
                                 NotifyCollectionChangedEventArgs e)
        {
            Navigated?.Invoke();
        }

        public ObservableStack<ViewModelBase> Journal { get; set; } =
            new ObservableStack<ViewModelBase>();

        public ViewModelBase CurrentTarget => Journal.Peek();

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action Navigated;

        public void Navigate<TWhere>() where TWhere : ViewModelBase
        {
            Journal.Push(
                Activator.CreateInstance<TWhere>());
        }

        public void NavigateBack()
        {
            _ = Journal.Pop();
        }

        public void NavigateToRoot()
        {
            while (CanNavigateBack())
            {
                NavigateBack();
            }
        }

        public void NavigateWithParameter<TWhere, TParam>(TParam param)
            where TWhere : ViewModelBase
        {
            Journal.Push(
                (ViewModelBase)
                Activator.CreateInstance(typeof(TParam),
                                         new object[] { param }));
        }

        public bool CanNavigateBack()
        {
            return Journal.Count > 1;
        }
    }
}
