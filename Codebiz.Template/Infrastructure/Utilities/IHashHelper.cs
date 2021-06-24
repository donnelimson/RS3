using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public interface IHashHelper
    {
        string ComputeHash(string input);
        string ComputeHash(Stream inputStream);
        string ComputeHash(byte[] inputStream);

        bool VerifyHash(string input, string hash);
        bool VerifyHash(Stream inputStream, string hash);
        bool VerifyHash(byte[] buffer, string hash);
    }

    public class HashHelper : IHashHelper
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public HashHelper(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public string ComputeHash(Stream inputStream)
        {
            return ByteArrayToHexadecimalString(_hashAlgorithm.ComputeHash(inputStream));
        }

        public string ComputeHash(byte[] buffer)
        {
            return ByteArrayToHexadecimalString(_hashAlgorithm.ComputeHash(buffer));
        }

        public string ComputeHash(string input)
        {
            return ByteArrayToHexadecimalString(_hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        public bool VerifyHash(string input, string hash)
        {
            return Compare(hash, ComputeHash(input));
        }

        public bool VerifyHash(Stream inputStream, string hash)
        {
            return Compare(hash, ComputeHash(inputStream));
        }

        public bool VerifyHash(byte[] buffer, string hash)
        {
            return Compare(hash, ComputeHash(buffer));
        }

        private bool Compare(string hash, string hashOfInput)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

        private string ByteArrayToHexadecimalString(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
