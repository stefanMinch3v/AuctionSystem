namespace AuctionSystem.Models.Utility
{
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
    }
}
