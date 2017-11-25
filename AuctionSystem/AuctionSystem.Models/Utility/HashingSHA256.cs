namespace AuctionSystem.Models.Utility
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;

    public static class HashingSHA256
    {
        public static string ComputeHash(string input)
        {
            SHA256 sha256 = new SHA256Managed();

            byte[] sha256Bytes = System.Text.Encoding.Default.GetBytes(input);

            byte[] cryString = sha256.ComputeHash(sha256Bytes);

            string sha256Str = string.Empty;

            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString("X");
            }

            return sha256Str;
        }

        public static void ValidateUserPassword(string password)
        {
            var minLength = 5;
            var maxLength = 100;
            bool userPasswordFlag = true;
            if (password.Length < minLength || password.Length > maxLength)
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsLower(c)))
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsUpper(c)))
            {
                userPasswordFlag = false;
            }

            if (!password.Any(c => char.IsDigit(c)))
            {
                userPasswordFlag = false;
            }

            if (!userPasswordFlag)
            {
                throw new ArgumentException("Password must contain at least 5 symbols, at most 100 symbols, a capital letter, small letter and a digit");
            }
        }
    }
}
