using HeladacWeb.Data;
using HeladacWeb.Models;
using MailKit;
using Microsoft.EntityFrameworkCore;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeladacWeb
{
    public static class Utility
    {
        public const int defaultPageSize = 20;
        public const int defaultPageIndex = 0;
        public static readonly PersonNameGenerator personNameGenerator = new PersonNameGenerator();
        public readonly static DateTimeOffset BeginningOfTime = new DateTimeOffset();
        static public DateTimeOffset _lastTimePhoneNumberList = new DateTimeOffset();
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

        public static DateTimeOffset getLastTimePhoneNumberList()
        {
            return _lastTimePhoneNumberList;
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
        /// <summary>
        /// Function generates a helm email. The first item is the email and the second is the username 
        /// </summary>
        /// <returns></returns>
        public static Tuple<string, string> generateHelmEmail()
        {
            string userName = RandomString();
            var domains = helmDomains;
            int domainIndex = random.Next(helmDomains.Length);
            string domainString = domains[domainIndex];
            string email = userName + "@" + domainString;
            Tuple<string, string> retValue = new Tuple<string, string>(email, userName);
            return retValue;
        }

        public static bool isBeginningOfTime(this DateTimeOffset time)
        {
            return time == BeginningOfTime;
        }

        public static string generateHelmPassword()
        {
            string retValue = RandomString(seedString: passwordChars);
            return retValue;
        }

        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly string passwordChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_)(*&^%$#@!{}:\" <>?,./;\'[]";
        private static HashSet<string> _helmDomains = new HashSet<string>( new string[] { "heladackid.com", "heladac.com" });

        public static string [] helmDomains { 
            get 
            {
                return _helmDomains.ToArray();
            } 
        }

        /// <summary>
        /// Function takes an email and returns a 4 item Tuple.
        /// Item1 is a boolean is true if <paramref name="input"/> is a valid email format and 
        /// Item2 is a boolean  is true if <paramref name="input"/> is part of heladac list of domains
        /// Item3 is a string of the username part of the <paramref name="input"/>
        /// Item4 is a string of the domain part of the <paramref name="input"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Tuple<bool, bool, string, string> isHeladacEmail(this string input)
        {
            bool isValidEmail = isEmail(input);
            bool isHeladacEmail = false;
            string usernameString = null;
            string domainString = null;
            if (isValidEmail)
            {
                var mailSplit = input.Split('@');
                usernameString = mailSplit[0].ToLower();
                domainString = mailSplit[1].ToLower();
                isHeladacEmail = _helmDomains.Contains(domainString);
            }

            Tuple<bool, bool, string, string> retValue = new Tuple<bool, bool, string, string>(isValidEmail, isHeladacEmail, usernameString, domainString);
            return retValue;
        }

        public static List<PhoneNumber> jayNumbers = new List<PhoneNumber>()
        {
            new PhoneNumber()
            {
              fullNumber= "+18059549725",
              isGeneral = true
            
            },
            new PhoneNumber()
            {
                fullNumber="+13478500836" //Jay numbers.
            }

        };

        public static List<PhoneNumber> _allPhoneNumbers = new List<PhoneNumber>() { 
        };

        async static public Task updateCachedPhoneNumbers(HeladacDbContext context)
        {
            var phoneNumbers =  await context.PhoneNumbers.Where(phoneNumber => phoneNumber.isGeneral).ToListAsync().ConfigureAwait(false);
            _allPhoneNumbers = phoneNumbers.ToList();
            _lastTimePhoneNumberList = DateTimeOffset.UtcNow;
        }

        public static T RandomEntry<T>(this IEnumerable<T> iterable) {
            int elementCount = iterable.Count();
            if (elementCount > 0)
            {
                if(elementCount == 1)
                {
                    return iterable.First();
                }
                int index = random.Next(0, elementCount);
                if (iterable is (IList<T>) )
                {
                    IList<T> listIter = (IList<T>)iterable;
                    return listIter[index];
                }
                return iterable.Take(index + 1).Last();
            }
            throw new ArgumentException("Cannot get a random value from an empty collection");
        }
    }
}
