using System.Threading.Tasks;
using System.Windows;

namespace PaintTintingDesktopApp.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public async Task<bool> AskAsync(string question)
        {
            return await Task.Run(() =>
            {
                return MessageBox.Show(question,
                                       "Вопрос",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question)
                    == MessageBoxResult.Yes;
            });
        }

        public async Task InformAsync(string information)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(information,
                                "Информация",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            });
        }

        public async Task InformErrorAsync(string error)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(error,
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            });
        }

        public async Task WarnAsync(string warning)
        {
            await Task.Run(() =>
            {
                MessageBox.Show(warning,
                                "Предупреждение",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            });
        }
    }
}
