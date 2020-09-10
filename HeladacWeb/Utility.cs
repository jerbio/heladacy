using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeladacWeb
{
    public static class Utility
    {
        /// <summary>
        /// Function takes a string and checks if the string is a valid email
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public bool isEmail(string value)
        {
            bool retValue = false;
            if (value.isNot_NullEmptyOrWhiteSpace())
            {
                string emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                Regex re = new Regex(emailRegex, RegexOptions.IgnoreCase);
                if (re.IsMatch(value))
                    retValue = true;
                else
                    retValue = false;
            }
            return retValue;
        }

        /// <summary>
        /// Funbction returns all emails in the string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public List<String> getMatchingEmails(string value)
        {
            List<String> retValue = new List<string>();
            if (value.isNot_NullEmptyOrWhiteSpace())
            {
                string emailRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

                Regex re = new Regex(emailRegex);
                foreach(Match match in re.Matches(value))
                {
                    retValue.Add(match.Value);
                }
            }
            return retValue;
        }


        public static bool isNot_NullEmptyOrWhiteSpace(this string input)
        {
            bool retValue = !(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input));
            return retValue;
        }

        /// <summary>
        /// got from http://stackoverflow.com/questions/16100/how-do-i-convert-a-string-to-an-enum-in-c
        /// Used to parse string to enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        private static Random random = new Random();
        public static string RandomString(int length = -1, string seedString=null)
        {
            if (length == -1)
            {
                length = random.Next(12, 32);
            }

            if (!seedString.isNot_NullEmptyOrWhiteSpace())
            {
                seedString = chars;
            }
            
            string retValue = new string(Enumerable.Repeat(seedString, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            retValue = retValue.ToLower();
            return retValue;
        }
        public static string generateHelmEmail()
        {
            string userName = RandomString();
            var domains = helmDomains;
            int domainIndex = random.Next(helmDomains.Length);
            string domainString = domains[domainIndex];
            string retValue = userName + "@" + domainString;
            return retValue;
        }

        public static string generateHelmPassword()
        {
            string retValue = RandomString(seedString: passwordChars);
            return retValue;
        }
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly string passwordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_)(*&^%$#@!{}:\" <>?,./;\'[]";
        private static string [] _helmDomains = new string[] { "heldackid.com", "heldac.com" };

        public static string [] helmDomains { 
            get 
            {
                return _helmDomains.ToArray();
            } 
        }
    }
}
