namespace AuctionSystem.Models.Utility
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string pattern = @"[a-z0-9A-Z]+[-._]*[a-z0-9A-Z]+@[a-z0-9A-Z]+([-][a-z0-9A-Z]+)*\.[a-z0-9A-Z]+([-][a-z0-9A-Z]+)*";
            string email = value as string;

            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(value.ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
