using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class UserDataStore : IDataStore<User>
    {
        public Task<bool> AddItemAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            using (PaintTintingBaseEntities entities = new PaintTintingBaseEntities())
            {
                try
                {
                    entities
                        .Entry(
                            entities.User.Find(item.Id)).CurrentValues
                        .SetValues(item);
                    if (await entities.SaveChangesAsync() > 0)
                    {
                        await DependencyService
                            .Get<IMessageBoxService>()
                            .InformAsync("Личный кабинет обновлён");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    await DependencyService
                        .Get<IMessageBoxService>()
                        .InformErrorAsync(ex);
                    return false;
                }
            }
        }
    }
}
