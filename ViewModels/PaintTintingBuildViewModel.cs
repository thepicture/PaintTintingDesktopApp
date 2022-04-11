﻿using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PaintTintingDesktopApp.ViewModels
{
    public class PaintTintingBuildViewModel : ViewModelBase
    {
        public PaintTintingBuildViewModel()
        {
            Title = "Страница создания краски";
            SearchTwoPaintingsAsync();
        }

        public SolidColorBrush ColorAsHex =>
            new SolidColorBrush(SelectedColor);

        private Color selectedColor = Color.FromRgb(255, 0, 0);

        public Color SelectedColor
        {
            get => selectedColor;
            set
            {
                if (SetProperty(ref selectedColor, value))
                {
                    OnPropertyChanged(
                        nameof(ColorAsHex));
                    SearchTwoPaintingsAsync();
                }
            }
        }

        private async void SearchTwoPaintingsAsync()
        {
            ICollection<Color> triadicColors = BlenderService
                .GetTriadicColors(SelectedColor);
            FirstTriadicColor = triadicColors.First();
            SecondTriadicColor = triadicColors.Last();
            await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    List<Paint> paintings = entities.Paint.ToList();

                    Color? firstClosestColor = ClosestColorService
                    .GetClosestColor(
                        paintings
                        .Select(p => (Color)ColorConverter
                        .ConvertFromString(p.ColorAsHex)),
                            FirstTriadicColor);
                    FirstFoundPaint = paintings.FirstOrDefault(p =>
                    {
                        return p.ColorAsHex.Contains(
                            firstClosestColor.ToString()
                                        .Substring(3));
                    });

                    Color? secondClosestColor = ClosestColorService
                   .GetClosestColor(
                       paintings
                       .Select(p => (Color)ColorConverter
                       .ConvertFromString(p.ColorAsHex)),
                           SecondTriadicColor);
                    SecondFoundPaint = paintings.FirstOrDefault(p =>
                    {
                        return p.ColorAsHex.Contains(
                            secondClosestColor.ToString()
                                        .Substring(3));
                    });
                }
            });
        }

        private Color firstTriadicColor;

        public Color FirstTriadicColor
        {
            get => firstTriadicColor;
            set => SetProperty(ref firstTriadicColor, value);
        }

        private Color secondTriadicColor;

        public Color SecondTriadicColor
        {
            get => secondTriadicColor;
            set => SetProperty(ref secondTriadicColor, value);
        }

        private Command mixColorsCommand;

        public ICommand MixColorsCommand
        {
            get
            {
                if (mixColorsCommand == null)
                {
                    mixColorsCommand = new Command(MixColorsAsync);
                }

                return mixColorsCommand;
            }
        }

        private async void MixColorsAsync()
        {
        }

        private Paint firstFoundPaint;

        public Paint FirstFoundPaint
        {
            get => firstFoundPaint;
            set => SetProperty(ref firstFoundPaint, value);
        }

        private Paint secondFoundPaint;

        public Paint SecondFoundPaint
        {
            get => secondFoundPaint;
            set => SetProperty(ref secondFoundPaint, value);
        }
    }
}