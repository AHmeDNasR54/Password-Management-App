using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass_project
{
    public class EncriptionUtilize
    {
        private static readonly string origionalChars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
            "abcdefghijklmnopqrstuvwxyz" +
            "0123456789" +
            "!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";
        private static readonly string altChars =
                "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz" +
                "0123456789" +
                "!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";

        public static string Encript(string password)
        {
            var sb = new StringBuilder();
            foreach (char c in password)
            {
                var charIndex = origionalChars.IndexOf(c);
                sb.Append(altChars[charIndex]);


            }
            return sb.ToString();
        }
        public static string Decript(string password)
        {
            var sb = new StringBuilder();
            foreach (char c in password)
            {
                var charIndex = altChars.IndexOf(c);
                sb.Append(origionalChars[charIndex]);


            }
            return sb.ToString();
        }
    }
}
