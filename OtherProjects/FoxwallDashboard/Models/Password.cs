// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Password.cs" company="Wordsmyth Games">
//   Copyright (C) 2010 by Linda Naughton
// </copyright>
// <summary>
//   Most of this class provided by http://www.aspheute.com/english/20040105.asp
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;

namespace FoxwallDashboard.Models
{
    public class Password
    {

        private readonly string _password;
        private readonly int _salt;

        public Password(string strPassword, int salt)
        {
            _password = strPassword;
            _salt = salt;
        }

        public static string CreateRandomPassword(int passwordLength)
        {
            const string AllowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            Byte[] randomBytes = new Byte[passwordLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            char[] chars = new char[passwordLength];
            int allowedCharCount = AllowedChars.Length;

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = AllowedChars[randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        public static int CreateRandomSalt()
        {
            Byte[] saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);

            return (saltBytes[0] << 24) + (saltBytes[1] << 16) +
                    (saltBytes[2] << 8) + saltBytes[3];
        }

        public string ComputeSaltedHash()
        {
            // Create Byte array of password string
            ASCIIEncoding encoder = new ASCIIEncoding();
            Byte[] secretBytes = encoder.GetBytes(_password);

            // Create a new salt
            Byte[] saltBytes = new Byte[4];
            saltBytes[0] = (byte)(_salt >> 24);
            saltBytes[1] = (byte)(_salt >> 16);
            saltBytes[2] = (byte)(_salt >> 8);
            saltBytes[3] = (byte)_salt;

            // append the two arrays
            Byte[] toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            Byte[] computedHash = sha1.ComputeHash(toHash);

            return encoder.GetString(computedHash);
        }        
    }
}
