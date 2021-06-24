using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public interface IPasswordHelper
    {
        string GenerateRandomPassword();
    }

    public class PasswordHelper : IPasswordHelper
    {
        public string GenerateRandomPassword()
        {
            int length = 6;

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }
    }
}
