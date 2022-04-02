using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PaintTintingDesktopApp.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public byte[] GetHash(string password, byte[] salt)
        {
            List<byte> passwordBytesAndSalt = Encoding.UTF8
                .GetBytes(password)
                .ToList();
            passwordBytesAndSalt.AddRange(salt);
            return SHA256
                .Create()
                .ComputeHash(
                    passwordBytesAndSalt.ToArray());
        }
    }
}
