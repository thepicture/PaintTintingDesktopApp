using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class ContactsDataStore : IDataStore<Contact>
    {
        public Task<bool> AddItemAsync(Contact item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Contact> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetItemsAsync(bool forceRefresh = false)
        {
            using (PaintTintingBaseEntities entities = new PaintTintingBaseEntities())
            {
                return await entities.Contact
                    .Include(c => c.Customer)
                    .ToListAsync();
            }
        }

        public Task<bool> UpdateItemAsync(Contact item)
        {
            throw new NotImplementedException();
        }
    }
}
