using Newtonsoft.Json;
using PaintTintingDesktopApp.Models.Constants;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Models.PartialModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class RegisterDataStore : IDataStore<RegisterUser>
    {
        public async Task<bool> AddItemAsync(RegisterUser item)
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
            if (string.IsNullOrWhiteSpace(item.LastName))
            {
                _ = errors.AppendLine("Введите фамилию");
            }
            if (string.IsNullOrWhiteSpace(item.FirstName))
            {
                _ = errors.AppendLine("Введите имя");
            }
            if (errors.Length > 0)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformErrorAsync(errors);
                return false;
            }

            byte[] salt = Encoding.UTF8.GetBytes(
                Guid.NewGuid()
                    .ToString())
                .Take(16)
                .ToArray();
            byte[] passwordHash = DependencyService
                .Get<IPasswordHashService>()
                .GetHash(item.PlainPassword, salt);
            item.Salt = salt;
            item.PasswordHash = passwordHash;
            User user = JsonConvert.DeserializeObject<User>(
                JsonConvert.SerializeObject(item));
            user.UserTypeId = UserTypes.Seller;
            bool isRegistered = await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    try
                    {
                        _ = entities.User.Add(user);
                        _ = entities.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.StackTrace);
                        return false;
                    }
                }
            });
            if (isRegistered)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Вы зарегистрированы");
            }
            else
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Регистрация не удалась. "
                    + "Проверьте подключение к базе данных");
            }
            return isRegistered;
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<RegisterUser> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegisterUser>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(RegisterUser item)
        {
            throw new NotImplementedException();
        }
    }
}
