using System.Text;

namespace BuildingLink.Core.Common.Helpers
{
    public static class StringHelpers
    {
        public static string RandomText(int length)
        {
            var randomText = new StringBuilder();

            const string allValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomText.Append(allValidChars[random.Next(allValidChars.Length)]);
            }

            return randomText.ToString();
        }

        public static string RandomNumber(int length)
        {
            var randomText = new StringBuilder();

            const string allValidChars = "0123456789";

            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomText.Append(allValidChars[random.Next(allValidChars.Length)]);
            }

            return randomText.ToString();
        }
    }
}
