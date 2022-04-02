﻿namespace PaintTintingDesktopApp.Services
{
    public interface INavigationService<TTarget>
    {
        TTarget CurrentTarget { get; }
        void NavigateBack();
        bool CanNavigateBack();
        void Navigate<TWhere>() where TWhere : TTarget;
        void NavigateWithParameter<TWhere, TParam>(TParam param) where TWhere : TTarget;
        void NavigateToRoot();
    }
}
