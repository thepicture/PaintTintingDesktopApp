using PaintTintingDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace PaintTintingDesktopApp.Services
{
    public class PaintDataStore : IDataStore<Paint>
    {
        public async Task<bool> AddItemAsync(Paint item)
        {
            StringBuilder errors = new StringBuilder();
            if (item.Paint1 == null)
            {
                _ = errors.AppendLine("Укажите первый родительский пигмент");
            }
            if (item.Paint11 == null)
            {
                _ = errors.AppendLine("Укажите второй родительский пигмент");
            }
            if (string.IsNullOrWhiteSpace(item.ProductName))
            {
                _ = errors.AppendLine("Введите название пигмента");
            }
            if (item.PaintProvider == null)
            {
                _ = errors.AppendLine("Укажите поставщика");
            }
            if (item.PackagingInLiters <= 0)
            {
                _ = errors.AppendLine("Введите " +
                    "положительное количество в литрах");
            }
            if (item.PriceInRubles <= 0)
            {
                _ = errors.AppendLine("Введите " +
                    "положительную стоимость в рублях");
            }
            if (errors.Length > 0)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformErrorAsync(errors);
                return false;
            }
            item.FirstParentId = item.Paint2.Id;
            item.SecondParentId = item.Paint3.Id;
            item.PaintProviderId = item.PaintProvider.Id;
            item.Paint2 = null;
            item.Paint3 = null;
            item.PaintProvider = null;
            bool isSaved = await Task.Run(() =>
            {
                using (PaintTintingBaseEntities entities =
                    new PaintTintingBaseEntities())
                {
                    try
                    {
                        _ = entities.Paint.Add(item);
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
            if (isSaved)
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Пигмент сохранён");
            }
            else
            {
                await DependencyService
                    .Get<IMessageBoxService>()
                    .InformAsync("Ошибка подключения к БД.  " +
                    "Пигмент не сохранён");
            }
            return isSaved;
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Paint> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Paint>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemAsync(Paint item)
        {
            throw new NotImplementedException();
        }
    }
}
