using CodingSeb.Localization;
using CSharpColorSpaceConverter;
using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Strategies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public PrescriptionViewModel(List<Paint> resultingAndFirstAndSecondColors)
        {
            Title = Loc.Tr(GetType().Name);
            ResultPaint = resultingAndFirstAndSecondColors[0];
            FirstPaint = resultingAndFirstAndSecondColors[1];
            SecondPaint = resultingAndFirstAndSecondColors[2];
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(ResultPaint.ColorAsHex);
            ColorName = ColorSpaceConverter.RGBToNamedColor(color.R, color.G, color.B).Split(new string[] { " (" }, StringSplitOptions.RemoveEmptyEntries)[0];
            ColorName += System.Environment.NewLine + ResultPaint.ColorAsHex;
            TotalPrice = (FirstPaint.PriceInRubles * FirstPaint.PackagingInLiters) + (SecondPaint.PriceInRubles * SecondPaint.PackagingInLiters);
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
            IPrintStrategy printStrategy = new PrintToPrinterStrategy();
            await PrintTagAsync(parameter, printStrategy);
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
            IPrintStrategy printStrategy = new PrintToPdfStrategy();
            await PrintTagAsync(parameter, printStrategy);
        }

        private async Task PrintTagAsync(object parameter, IPrintStrategy printStrategy)
        {
            await printStrategy.PrintAsync(parameter, MessageBoxService);
        }

        private string colorName;
        public string ColorName
        {
            get => colorName;
            set => SetProperty(ref colorName, value);
        }
    }
}
