using Newtonsoft.Json;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using System;
using System.Text;

namespace PaintTintingDesktopApp.Services
{
    public class SessionService : ISessionService<User>
    {
        public User PermanentIdentity
        {
            get => JsonConvert.DeserializeObject<User>(
                    Encoding.UTF8.GetString(
                        Convert.FromBase64String(
                            Settings.Default.UserBase64)),
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling
                                    .Ignore
                        });

            set
            {
                if (value == null)
                {
                    Settings.Default.UserBase64 = string.Empty;
                    Settings.Default.Save();
                    return;
                }
                string userBase64 = Convert.ToBase64String(
              Encoding.UTF8.GetBytes(
                  JsonConvert.SerializeObject(value, new JsonSerializerSettings
                  {
                      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                  })));
                Settings.Default.UserBase64 = userBase64;
                Settings.Default.Save();
            }
        }

        public User TemporaryIdentity { get; set; }
    }
}
