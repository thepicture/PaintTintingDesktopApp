using CodingSeb.Localization;
using PaintTintingDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaintTintingDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactsViewModel : ViewModelBase
    {
        public ContactsViewModel()
        {
            Title = Loc.Tr(
                GetType().Name);
            LoadContactsAsync();
        }

        public async void LoadContactsAsync()
        {
            IEnumerable<Contact> currentContacts =
                await ContactsDataStore.GetItemsAsync();
            if (!string.IsNullOrWhiteSpace(PhoneSearchText))
            {
                currentContacts = currentContacts.Where(s =>
                {
                    return s.PhoneNumber.StartsWith(PhoneSearchText);
                });
            }
            Contacts = new ObservableCollection<Contact>(currentContacts);
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private string phoneSearchText;

        public string PhoneSearchText
        {
            get => phoneSearchText; set
            {
                if (SetProperty(ref phoneSearchText, value))
                {
                    LoadContactsAsync();
                }
            }
        }
    }
}