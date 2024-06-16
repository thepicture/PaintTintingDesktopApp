using PaintTintingDesktopApp.Services;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PaintTintingDesktopApp.Models.Factories
{
    public static class PrintDocumentFactory
    {
        public static PrintDocument CreatePrintDocument(string printerName, object parameter, IMessageBoxService notifier)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = printerName;
            printDocument.EndPrint += async (sender, e) =>
            {
                await notifier.InformAsync("Этикетка отправлена на печать");
            };
            printDocument.PrintPage += (sender, e) =>
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
                }
            };
            return printDocument;
        }
    }
}
