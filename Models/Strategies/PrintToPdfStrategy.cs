using PaintTintingDesktopApp.Services;
using System;
using System.Globalization;
using System.Printing;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PaintTintingDesktopApp.Strategies
{
    public class PrintToPdfStrategy : IPrintStrategy
    {
        public async Task PrintAsync(object parameter, IMessageBoxService notifier)
        {
            try
            {
                using (PrintServer printServer = new PrintServer())
                {
                    CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
                    string printQueueName = "Microsoft Print to PDF";
                    PrintDialog printDialog = new PrintDialog
                    {
                        PrintQueue = new PrintQueue(printServer, printQueueName),
                    };
                    printDialog.PrintVisual((Visual)parameter, "Ticket print to pdf");
                    await notifier.InformAsync(cultureInfo.Name.Contains("en-") ? "Ticket has been printed" : "Этикетка распечатана");
                }
            }
            catch (Exception ex)
            {
                await notifier.InformErrorAsync("Не удалось сохранить в pdf Формат. Причина: " + ex);
            }
        }
    }
}
