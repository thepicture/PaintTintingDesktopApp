using Newtonsoft.Json;
using PaintTintingDesktopApp.Models.Entities;
using PaintTintingDesktopApp.Properties;
using System;
using System.Text;

namespace PaintTintingDesktopApp.Services
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class SessionService : ISessionService<User>
    {
        private User temporaryIdentity;

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
                TemporaryIdentity = value;
                string serializedUser = JsonConvert
                    .SerializeObject(value, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                byte[] serializedUserBytes = Encoding.UTF8.GetBytes(
                                  serializedUser);
                string userBase64 = Convert.ToBase64String(serializedUserBytes);
                Settings.Default.UserBase64 = userBase64;
                Settings.Default.Save();
            }
        }

        public User TemporaryIdentity
        {
            get
            {
                if (PermanentIdentity is User user)
                {
                    return user;
                }
                else
                {
                    return temporaryIdentity;
                }
            }
            set => temporaryIdentity = value;
        }

        public void Abandon()
        {
            Settings.Default.UserBase64 = string.Empty;
            Settings.Default.Save();
            TemporaryIdentity = null;
        }
    }
}
