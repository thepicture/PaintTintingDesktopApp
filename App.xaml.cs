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
            try
            {
                using (PaintTintingBaseEntities entities
                    = new PaintTintingBaseEntities())
                {
                    entities.Database.Connection.Open();
                    if (!entities.User.Any(u => u.Login == "seller"))
                    {
                        InsertUser(entities);
                    }
                }
            }
            catch (Exception ex)
            {
                new MessageBoxService()
                    .InformErrorAsync("Запуск приложения "
                    + "невозможен, так как "
                    + "база данных недоступна. "
                    + "Проверьте подключение "
                    + "и перезапустите приложение. "
                    + ex)
                        .Wait();
            }

            base.OnStartup(e);
            DependencyService.Register<NavigationService>();
            DependencyService.Register<PasswordHashService>();
            DependencyService.Register<MessageBoxService>();
            DependencyService.Register<LoginDataStore>();
            DependencyService.Register<RegisterDataStore>();
            DependencyService.Register<SessionService>();
            DependencyService.Register<BlenderService>();
            DependencyService.Register<ClosestColorService>();
            DependencyService.Register<PaintDataStore>();
            DependencyService.Register<UserDataStore>();

            DependencyService.Get<INavigationService<ViewModelBase>>()
                   .Navigate<LoginViewModel>();
            if (!string.IsNullOrWhiteSpace(
                Settings.Default.UserBase64))
            {
                DependencyService.Get<INavigationService<ViewModelBase>>()
                   .Navigate<ControlPanelViewModel>();
            }
        }

        private void InsertUser(PaintTintingBaseEntities entities)
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
            _ = entities.User.Add(user);
            _ = entities.SaveChanges();
        }
    }
}
