using CodingSeb.Localization;
using PaintTintingDesktopApp.Commands;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Models.Factories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace PaintTintingDesktopApp.ViewModels
{
    public class ColorMixViewModel : ViewModelBase
    {
        public ColorMixViewModel()
        {
            Title = Loc.Tr(
                GetType().Name);
            ResultPaint = PaintFactory.CreatePaint("ffffff");
            ResultPaint.PropertyChanged += OnResultPaintChanged;
            InsertProvidersAsync();
            InsertParentsAsync();
        }

        private void OnResultPaintChanged(object sender,
                                          PropertyChangedEventArgs e)
        {
            if (ResultPaint.Paint2 == null
                || ResultPaint.Paint3 == null)
            {
                return;
            }
            Color firstHexColor = (Color)ColorConverter
                .ConvertFromString(ResultPaint.Paint2.ColorAsHex);
            Color secondHexColor = (Color)ColorConverter
                .ConvertFromString(ResultPaint.Paint3.ColorAsHex);
            Color resultingColor = BlenderService
                .Mix(firstHexColor, secondHexColor);
            ResultPaint.ColorAsHex = "#FF"
                + resultingColor.R.ToString("x2")
                + resultingColor.G.ToString("x2")
                + resultingColor.B.ToString("x2");
        }

        private async void InsertParentsAsync()
        {
            using (PaintTintingBaseEntities entities
                = new PaintTintingBaseEntities())
            {
                List<Paint> providersFromDatabase
                    = await entities.Paint
                    .Include(p => p.PaintProvider)
                    .ToListAsync();
                Parents = new ObservableCollection<Paint>
                    (providersFromDatabase);
                ResultPaint.Paint2 = Parents.FirstOrDefault();
                ResultPaint.Paint3 = Parents.FirstOrDefault();
            }
        }

        private async void InsertProvidersAsync()
        {
            using (PaintTintingBaseEntities entities
                = new PaintTintingBaseEntities())
            {
                List<PaintProvider> providersFromDatabase
                    = await entities.PaintProvider.ToListAsync();
                Providers = new ObservableCollection<PaintProvider>
                    (providersFromDatabase);
            }
        }

        private ObservableCollection<PaintProvider> providers;

        public ObservableCollection<PaintProvider> Providers
        {
            get => providers;
            set => SetProperty(ref providers, value);
        }

        private ObservableCollection<Paint> parents;

        public ObservableCollection<Paint> Parents
        {
            get => parents;
            set => SetProperty(ref parents, value);
        }

        private Paint resultPainting;

        public Paint ResultPaint
        {
            get => resultPainting;
            set => SetProperty(ref resultPainting, value);
        }

        private Command savePaintingCommand;

        public ICommand SavePaintingCommand
        {
            get
            {
                if (savePaintingCommand == null)
                {
                    savePaintingCommand = new Command(SavePaintingAsync);
                }

                return savePaintingCommand;
            }
        }

        private async void SavePaintingAsync()
        {
            if (await PaintDataStore.AddItemAsync(ResultPaint))
            {
                NavigationService.NavigateBack();
            }
        }
    }
}