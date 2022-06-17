using CodingSeb.Localization;
using CodingSeb.Localization.Loaders;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using PaintTintingDesktopApp.Services;
using PaintTintingDesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace PaintTintingDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string DataSourcesPath = "./../../DataSources.txt";

        public static string Connection { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ShutDownIfDataSourcesAreInvalid();

            LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
            LocalizationLoader.Instance.AddFile(@".\..\..\translations.loc.json");
            Loc.Instance.CurrentLanguage = Settings.Default.CurrentLanguage;

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
            DependencyService.Register<ServicesDataStore>();
            DependencyService.Register<ContactsDataStore>();

            DependencyService.Get<INavigationService<ViewModelBase>>()
                   .Navigate<LoginViewModel>();
            if (!string.IsNullOrWhiteSpace(
                Settings.Default.UserBase64))
            {
                DependencyService.Get<INavigationService<ViewModelBase>>()
                   .Navigate<ControlPanelViewModel>();
            }
        }

        private void ShutDownIfDataSourcesAreInvalid()
        {
            if (!IsNoneOfDataSourcesWorks())
            {
                return;
            }
            else
            {
                MessageBox.Show("Работа программы невозможна. "
                                + "Все строки "
                                + "подключения недоступны "
                                + "для связи с хранилищем данных");
                Shutdown();
            }
        }

        private static bool IsNoneOfDataSourcesWorks()
        {
            // File must contain line-by-line data sources. Looping from the last to the first.
            Stack<string> sources = new Stack<string>(
                File.ReadAllLines(DataSourcesPath));
            while (sources.Count > 0)
            {
                string connection = default;
                try
                {
                    connection = $@"metadata=res://*/Models.Entities.BaseModel.csdl|res://*/Models.Entities.BaseModel.ssdl|res://*/Models.Entities.BaseModel.msl;
                                    provider=System.Data.SqlClient;
                                    provider connection string="";
                                    data source={sources.Pop()};
                                    initial catalog=PaintTintingBase;
                                    integrated security=True;
                                    MultipleActiveResultSets=True;
                                    App=EntityFramework""";
                    using (PaintTintingBaseEntities entities = new PaintTintingBaseEntities(connection))
                    {
                        entities.Database.Connection.Open();
                    }
                    Connection = connection;
                    return false;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Connection string "
                                            + connection
                                            + " is not working: "
                                            + ex.ToString());
                    continue;
                }
            }
            return true;
        }
    }
}
