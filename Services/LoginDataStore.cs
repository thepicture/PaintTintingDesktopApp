using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class LoginDataStore : IDataStore<User>
    {
        public async Task<bool> AddItemAsync(User item)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(item.Login))
            {
                _ = errors.AppendLine("Введите логин");
            }
            if (string.IsNullOrWhiteSpace(item.PlainPassword))
            {
                _ = errors.AppendLine("Введите пароль");
            }
            if (errors.Length > 0)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformErrorAsync(
                    errors.ToString());
                return false;
            }
            bool isAuthenticated = await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    User dbUser = entities.User
                        .AsNoTracking()
                        .FirstOrDefault(u => u.Login == item.Login);
                    if (dbUser == null)
                    {
                        return false;
                    }
                    byte[] salt = dbUser.Salt;
                    byte[] passwordHash = DependencyService
                        .Get<IPasswordHashService>()
                        .GetHash(item.PlainPassword, salt);
                    return Enumerable.SequenceEqual(dbUser.PasswordHash,
                                                    passwordHash);
                }
            });
            if (isAuthenticated)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Вы авторизованы");
            }
            else
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Неверный логин или пароль");
            }
            return isAuthenticated;
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

        public Task<bool> UpdateItemAsync(User item)
        {
            throw new NotImplementedException();
        }
    }
}
