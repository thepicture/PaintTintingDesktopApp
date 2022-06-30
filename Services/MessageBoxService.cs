using System.Globalization;
using System.Threading.Tasks;
using System.Windows;

namespace PaintTintingDesktopApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public async Task<bool> AskAsync(object question)
        {
            string questionAsString = question.ToString();
            return await Task.Run(() =>
            {
                return MessageBox.Show(questionAsString,
                                       CultureInfo.CurrentUICulture.Name.Contains("ru-") ? "Вопрос" : "Question",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question)
                    == MessageBoxResult.Yes;
            });
        }

        public async Task InformAsync(object information)
        {
            string informationAsString = information.ToString();
            await Task.Run(() =>
            {
                return MessageBox.Show(informationAsString,
                                CultureInfo.CurrentUICulture.Name.Contains("ru-") ? "Информация" : "Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            });
        }

        public async Task InformErrorAsync(object error)
        {
            string errorAsString = error.ToString();
            await Task.Run(() =>
            {
                return MessageBox.Show(errorAsString,
                                CultureInfo.CurrentUICulture.Name.Contains("ru-") ? "Ошибка" : "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            });
        }

        public async Task WarnAsync(object warning)
        {
            string warningAsString = warning.ToString();
            await Task.Run(() =>
            {
                return MessageBox.Show(warningAsString,
                                       CultureInfo.CurrentUICulture.Name.Contains("ru-") ? "Предупреждение" : "Warning",
                                       MessageBoxButton.OK,
                                       MessageBoxImage.Warning);
            });
        }
    }
}
