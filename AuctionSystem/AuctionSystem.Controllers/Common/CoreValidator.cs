namespace AuctionSystem.Controllers.Common
{
    using System;

    public static class CoreValidator
    {
        // for specific case if we need to check the object
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty.");
            }
        }

        public static void ThrowIfNegativeOrZero(int number, string name)
        {
            if (number <= 0)
            {
                throw new ArgumentException($"{name} cannot be negative or zero.");
            }
        }

        public static void ThrowIfNegative(int number, string name)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{name} cannot be negative.");
            }
        }


        public static void ThrowIfNegativeOrZero(decimal number, string name)
        {
            if (number <= 0)
            {
                throw new ArgumentException($"{name} cannot be negative or zero.");
            }
        }

        public static void ThrowIfDateIsNotCorrect(string date, string name)
        {
            if (!DateTime.TryParse(date, out DateTime temp))
            {
                throw new ArgumentException($"{name} is not in the valid format [dd-mm-yyyy, dd/mm/yyyy].");
            }
        }
        
        // TODO checks when the Zip and Payment controllers are done!
    }
}
