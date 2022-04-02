using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using PaintTintingDesktopApp.Services;
using PaintTintingDesktopApp.ViewModels;
using System;
using System.Linq;
using System.Windows;

namespace PaintTintingDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DependencyService.Register<NavigationService>();
            DependencyService.Register<PasswordHashService>();
            DependencyService.Register<MessageBoxService>();

            if (Settings.Default.IsAuthenticated)
            {

            }
            else
            {
                DependencyService.Get<INavigationService<ViewModelBase>>()
                    .Navigate<LoginViewModel>();
            }
        }

        void InsertUser()
        {
            using (PaintTintingBaseEntities entities =
                new PaintTintingBaseEntities())
            {
                byte[] salt = Guid
                    .NewGuid()
                    .ToByteArray()
                    .Take(16)
                    .ToArray();
                byte[] passwordHash = DependencyService
                    .Get<IPasswordHashService>()
                    .GetHash("123", salt);
                User user = new User
                {
                    UserType = entities.UserType.First(t => t.Title == "Продавец"),
                    LastName = "Иванов",
                    FirstName = "Иван",
                    Patronymic = "Иванович",
                    Login = "seller",
                    PasswordHash = passwordHash,
                    Salt = salt
                };
                entities.User.Add(user);
                entities.SaveChanges();
            }
        }
    }
}
