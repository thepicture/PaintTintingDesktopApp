using PaintTintingDesktopApp.ViewModels;
using System;

namespace PaintTintingDesktopApp.Services
{
    public class NavigationService : INavigationService<ViewModelBase>
    {
        private readonly ObservableStack<ViewModelBase> journal =
            new ObservableStack<ViewModelBase>();

        public ViewModelBase CurrentTarget => journal.Peek();

        public void Navigate<TWhere>() where TWhere : ViewModelBase
        {
            journal.Push(
                Activator.CreateInstance<TWhere>());
        }

        public void NavigateBack()
        {
            _ = journal.Pop();
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
            journal.Push(
                (ViewModelBase)
                Activator.CreateInstance(typeof(TParam),
                                         new object[] { param }));
        }

        public bool CanNavigateBack()
        {
            return journal.Count > 1;
        }
    }
}
