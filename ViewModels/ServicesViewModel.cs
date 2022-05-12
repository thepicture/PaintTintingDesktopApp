using CodingSeb.Localization;
using PaintTintingDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintTintingDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ServicesViewModel : ViewModelBase
    {
        public ServicesViewModel()
        {
            Title = Loc.Tr(
                GetType().Name);
            LoadServicesAsync();
        }

        public async void LoadServicesAsync()
        {
            IEnumerable<Service> currentServices =
                await ServicesDataStore.GetItemsAsync();
            if (!string.IsNullOrWhiteSpace(TitleSearchText))
            {
                currentServices = currentServices.Where(s =>
                {
                    return s.Title.StartsWith(TitleSearchText, System.StringComparison.OrdinalIgnoreCase);
                });
            }
            if (!string.IsNullOrWhiteSpace(PriceSearchText)
                && decimal.TryParse(PriceSearchText, out decimal price))
            {
                currentServices = currentServices.Where(s =>
                {
                    return s.PriceInRubles <= price;
                });
            }
            Services = new ObservableCollection<Service>(currentServices);
        }

        public ObservableCollection<Service> Services { get; set; }

        private string priceSearchText;
        private string titleSearchText;

        public string PriceSearchText
        {
            get => priceSearchText; set
            {
                if (SetProperty(ref priceSearchText, value))
                {
                    LoadServicesAsync();
                }
            }
        }

        public string TitleSearchText
        {
            get => titleSearchText; set
            {
                if (SetProperty(ref titleSearchText, value))
                {
                    LoadServicesAsync();
                }
            }
        }
    }
}