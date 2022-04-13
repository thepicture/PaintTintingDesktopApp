using CSharpColorSpaceConverter;
using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaintTintingDesktopApp.ViewModels
{
    public class PrescriptionViewModel : ViewModelBase
    {
        private Paint resultPaint;
        private Paint firstPaint;
        private Paint secondPaint;

        public Paint ResultPaint
        {
            get => resultPaint;
            set => SetProperty(ref resultPaint, value);
        }
        public Paint FirstPaint
        {
            get => firstPaint;
            set => SetProperty(ref firstPaint, value);
        }
        public Paint SecondPaint
        {
            get => secondPaint;
            set => SetProperty(ref secondPaint, value);
        }
        public PrescriptionViewModel
            (List<Paint> resultingAndFirstAndSecondColors)
        {
            Title = "Рецептура смеси";
            ResultPaint = resultingAndFirstAndSecondColors[0];
            FirstPaint = resultingAndFirstAndSecondColors[1];
            SecondPaint = resultingAndFirstAndSecondColors[2];
            System.Windows.Media.Color color
                = (System.Windows.Media.Color)System.Windows.Media
                .ColorConverter
                .ConvertFromString(ResultPaint.ColorAsHex);
            ColorName = ColorSpaceConverter.RGBToNamedColor(
                color.R,
                color.G,
                color.B)
                .Split(
                new string[] { " (" },
                StringSplitOptions.RemoveEmptyEntries)[0];
            ColorName += System.Environment.NewLine + ResultPaint.ColorAsHex;
            TotalPrice
                = (FirstPaint.PriceInRubles * FirstPaint.PackagingInLiters)
                + (SecondPaint.PriceInRubles * SecondPaint.PackagingInLiters);
        }

        private decimal totalPrice;

        public decimal TotalPrice
        {
            get => totalPrice;
            set => SetProperty(ref totalPrice, value);
        }

        private Command printTagToPrinterCommand;

        public ICommand PrintTagToPrinterCommand
        {
            get
            {
                if (printTagToPrinterCommand == null)
                {
                    printTagToPrinterCommand = new Command(PrintTagToPrinterAsync);
                }

                return printTagToPrinterCommand;
            }
        }

        private async void PrintTagToPrinterAsync(object parameter)
        {
            PrinterSettings.StringCollection printers
                = PrinterSettings.InstalledPrinters;
            if (printers.Count == 0)
            {
                await MessageBoxService.InformErrorAsync(
                    "Не подключен принтер. "
                    + "Печать отменена");
                return;
            }
            string selectedPrinterName = null;
            foreach (string printerName in printers)
            {
                if (await MessageBoxService.AskAsync(
                    $"Печатать на принтере {printerName}?"))
                {
                    selectedPrinterName = printerName;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(selectedPrinterName))
            {
                await MessageBoxService.InformErrorAsync("Вы не выбрали " +
                    "ни одного принтера. Печать отменена");
                return;
            }
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = selectedPrinterName;
            printDocument.EndPrint += delegate (object sender, PrintEventArgs e)
            {
                MessageBoxService.InformAsync("Этикетка отправлена на печать");
            };
            printDocument.PrintPage += delegate (object sender, PrintPageEventArgs e)
            {
                RenderTargetBitmap bitmap = new RenderTargetBitmap(
                    (int)((FrameworkElement)parameter).ActualWidth,
                    (int)((FrameworkElement)parameter).ActualHeight,
                    96,
                    96,
                    PixelFormats.Pbgra32);
                bitmap.Render((FrameworkElement)parameter);
                BitmapFrame frame = BitmapFrame.Create(bitmap);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(frame);
                using (MemoryStream stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                    e.Graphics.DrawImage(image, new PointF(0, 0));
                };
            };
            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await MessageBoxService.InformErrorAsync("Не удалось распечатать. " +
                    "Причина: " + ex.StackTrace);
            }
        }

        private Command printTagToPdfCommand;

        public ICommand PrintTagToPdfCommand
        {
            get
            {
                if (printTagToPdfCommand == null)
                {
                    printTagToPdfCommand = new Command(PrintTagToPdfAsync);
                }

                return printTagToPdfCommand;
            }
        }

        private async void PrintTagToPdfAsync(object parameter)
        {
            using (PrintServer printServer = new PrintServer())
            {
                PrintDialog printDialog = new PrintDialog
                {
                    PrintQueue = new PrintQueue(printServer,
                                                "Microsoft Print To PDF"),
                };
                printDialog.PrintVisual((Visual)parameter,
                                        "Печать этикетки в .pdf");
                await MessageBoxService.InformAsync("Этикетка распечатана");
            }
        }

        private string colorName;

        public string ColorName
        {
            get => colorName;
            set => SetProperty(ref colorName, value);
        }
    }
}