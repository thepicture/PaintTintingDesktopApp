using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class ServicesDataStore : IDataStore<Service>
    {
        public Task<bool> AddItemAsync(Service item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Service> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Service>> GetItemsAsync(bool forceRefresh = false)
        {
            using (PaintTintingBaseEntities entities = new PaintTintingBaseEntities())
            {
                return await entities.Service.ToListAsync();
            }
        }

        public Task<bool> UpdateItemAsync(Service item)
        {
            throw new NotImplementedException();
        }
    }
}
