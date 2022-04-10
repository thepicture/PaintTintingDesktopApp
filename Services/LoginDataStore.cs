using Newtonsoft.Json;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Models.PartialModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class LoginDataStore : IDataStore<LoginUser>
    {
        public async Task<bool> AddItemAsync(LoginUser item)
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
                    User dbLoginUser = entities.User
                        .AsNoTracking()
                        .FirstOrDefault(u => u.Login == item.Login);
                    if (dbLoginUser == null)
                    {
                        return false;
                    }
                    byte[] salt = dbLoginUser.Salt;
                    byte[] passwordHash = DependencyService
                        .Get<IPasswordHashService>()
                        .GetHash(item.PlainPassword, salt);
                    return Enumerable.SequenceEqual(dbLoginUser.PasswordHash,
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

        public async Task<LoginUser> GetItemAsync(string id)
        {
            return await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                new PaintTintingBaseEntities())
                {
                    User user = entities.User
                    .FirstOrDefault(u =>
                        u.Login.Equals(id,
                                       StringComparison.OrdinalIgnoreCase));
                    JsonSerializerSettings settings =
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        };
                    LoginUser loginUser = JsonConvert.DeserializeObject<LoginUser>(
                        JsonConvert.SerializeObject(user, settings));
                    return loginUser;
                }
            });
        }

        public Task<IEnumerable<LoginUser>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(LoginUser item)
        {
            throw new NotImplementedException();
        }
    }
}
