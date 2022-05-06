﻿using System.Threading.Tasks;
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
                                       "Вопрос",
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
                                "Информация",
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
                                "Ошибка",
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
                                       "Предупреждение",
                                       MessageBoxButton.OK,
                                       MessageBoxImage.Warning);
            });
        }
    }
}
