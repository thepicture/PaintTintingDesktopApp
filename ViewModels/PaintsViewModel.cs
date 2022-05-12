using PaintTintingDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintTintingDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class PaintsViewModel : ViewModelBase
    {
        public PaintsViewModel()
        {
            Title = "Просмотр каталога цвета";
        }

        public async void LoadPaintsAsync()
        {
            IEnumerable<Paint> currentPaints =
                await PaintDataStore.GetItemsAsync();
            if (!string.IsNullOrWhiteSpace(ColorAsHexSearchText))
            {
                currentPaints = currentPaints.Where(s =>
                {
                    return s.ColorAsHex.Contains(ColorAsHexSearchText);
                });
            }
            if (!string.IsNullOrWhiteSpace(PriceSearchText)
                && decimal.TryParse(PriceSearchText, out decimal price))
            {
                currentPaints = currentPaints.Where(s =>
                {
                    return s.PriceInRubles <= price;
                });
            }
            Paints = new ObservableCollection<Paint>(currentPaints);
        }

        public ObservableCollection<Paint> Paints { get; set; }

        private string priceSearchText;
        private string HexAsStringSearchText;

        public string PriceSearchText
        {
            get => priceSearchText; set
            {
                if (SetProperty(ref priceSearchText, value))
                {
                    LoadPaintsAsync();
                }
            }
        }

        public string ColorAsHexSearchText
        {
            get => HexAsStringSearchText; set
            {
                if (SetProperty(ref HexAsStringSearchText, value))
                {
                    LoadPaintsAsync();
                }
            }
        }
    }
}