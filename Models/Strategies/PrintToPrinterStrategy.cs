using PaintTintingDesktopApp.Models.Factories;
using PaintTintingDesktopApp.Services;
using PaintTintingDesktopApp.ViewModels;
using System;
using System.Drawing.Printing;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Strategies
{
    public class PrintToPrinterStrategy : IPrintStrategy
    {
        public async Task PrintAsync(object parameter, IMessageBoxService notifier)
        {
            PrinterSettings.StringCollection printers = PrinterSettings.InstalledPrinters;
            if (printers.Count == 0)
            {
                await notifier.InformErrorAsync("Не подключен принтер. Печать отменена");
                return;
            }
            string selectedPrinterName = null;
            foreach (string printerName in printers)
            {
                if (await notifier.AskAsync($"Печатать на принтере {printerName}?"))
                {
                    selectedPrinterName = printerName;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(selectedPrinterName))
            {
                await notifier.WarnAsync("Вы не выбрали принтер. Печать отменена");
                return;
            }
            PrintDocument printDocument = PrintDocumentFactory.CreatePrintDocument(selectedPrinterName, parameter, notifier);
            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                await notifier.InformErrorAsync("Не удалось распечатать. Причина: " + ex);
            }
        }
    }
}
