using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace TetrisConnection
{
    public static class Encrypter
    {
        static SHA512CryptoServiceProvider cryptoService = new SHA512CryptoServiceProvider();
        public static string PasswordEncrypt(string password)
        {
            return Convert.ToBase64String(cryptoService.ComputeHash(Encoding.ASCII.GetBytes(password)));
        }
    }
}