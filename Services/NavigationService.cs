using PaintTintingDesktopApp.ViewModels;
using System;
using System.Collections.Specialized;
using System.Linq;

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
            while (Journal.Count > 1)
            {
                NavigateBack();
            }
        }

        public void NavigateWithParameter<TWhere, TParam>(TParam param)
            where TWhere : ViewModelBase
        {
            Journal.Push(
                (ViewModelBase)
                Activator.CreateInstance(typeof(TWhere),
                                         new object[] { param }));
        }

        public bool CanNavigateBack()
        {
            if (Journal.Count < 2)
            {
                return false;
            }
            ViewModelBase viewModel = Journal
                .ElementAt(1);
            return !(viewModel is LoginViewModel)
                   || Journal.Peek() is RegisterViewModel;
        }
    }
}
