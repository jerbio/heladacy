using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Immutable;
using HeladacWeb.AppLogic;

namespace HeladacWeb.Models
{
    public class CreditCard
    {
        protected string _id = Guid.NewGuid().ToString();
        protected HeladacUser _heladacUser;
        [Key]
        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public CreditCard()
        {

        }

        public CreditCard(HelmUser helmUser)
        {
            firstName = helmUser.firstName;
            lastName = helmUser.lastName;
            address1 = helmUser.address1;
            address2 = helmUser.address2;
            state = helmUser.state;
            city = helmUser.city;
            country = helmUser.country;
            postal = helmUser.postal;
            
        }

        public void autoPopulateCredentials(HeladacUser heladacUser)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            CreditCardConfig creditCardConfig = CreditCardUtility.creditCardConfigs.RandomEntry();
            ccNumber = CC_Generator.generateCreditCardNumber(creditCardConfig);
            CityPostal cityPostal = CC_Generator.generateRandomPostalCode(creditCardConfig, creditCardConfig.country);
            postal ??= cityPostal.postal;
            country ??= creditCardConfig.country;
            cvvCode ??= CC_Generator.generateRandomCvv(creditCardConfig);
            expiryYear = now.Year + 4;
            expiryMonth = (((now.Month + Utility.random.Next(0,5))%12)+1).ToString();
            phoneNumber = heladacUser.latestPhoneNumber.fullNumber;
        }


        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string misc { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postal { get; set; }
        public string ccNumber { get; set; }
        public string cvvCode { get; set; }
        public string phoneNumber { get; set; }
        public string expiryMonth { get; set; }
        public int expiryYear { get; set; }
        [Required]
        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser_db { get { return _heladacUser; } set { _heladacUser = value; } }

    }

    static class CC_Generator
    {
        private static Random random = new Random();
        public static string generateCreditCardNumber(CreditCardConfig config)
        {
            string retValue = null;
            if (config.prefixes.Count > 0)
            {
                List<string> prefixes = config.prefixes.ToList();
                string prefix = prefixes[random.Next(0, prefixes.Count)];
                retValue = generateRandomCCNumber(prefix, config.digitCount);
            }
            else
            {
                retValue = generateRandomCCNumber("", config.digitCount);
            }

            return retValue;
        }

        /// <summary>
        /// Function generates a credit card number which is LUN compatible.
        /// </summary>
        /// <param name="prefix">The initial set of numbers needed before the rest of a credit card is generated</param>
        /// <param name="digitCount"> The number of credit card digits needed to be generated. The default is 16</param>
        /// <returns></returns>
        static string generateRandomCCNumber(string prefix, int digitCount = 16)
        {
            if (digitCount <= 1)
            {
                throw new ArgumentException("The digitCount needs to be greater than 1");
            }

            int ccCountLimit = digitCount - 1;
            string ccNumber = prefix;

            while (ccNumber.Length < ccCountLimit)
            {
                int nextInt = random.Next(0, 10);
                ccNumber += nextInt;
            }
            int lun = generateLUN(ccNumber);
            string retValue = ccNumber + lun;
            return retValue;

        }

        /// <summary>
        /// Followed directions from https://www.dcode.fr/luhn-algorithm#f1
        /// </summary>
        /// <param name="digits"></param>
        /// <returns>An integer which is the LUN number</returns>
        static int generateLUN(string digits)
        {
            string digitsReversed = ReverseString(digits);
            int i = 0;
            int sum = 0;
            while (i < digitsReversed.Length)
            {
                bool isEVen = i % 2 == 0;
                int digit = Convert.ToInt32("" + digitsReversed[i]);
                int transformed = digit;
                if (isEVen)
                {
                    transformed = digit * 2;
                }
                if (transformed >= 10)
                {
                    transformed = (transformed / 10) + (transformed % 10);
                }

                sum += transformed;
                i++;
            }

            int controlDigit = ((10 - (sum % 10)) % 10);
            return controlDigit;
        }

        public static string generateRandomCvv(CreditCardConfig creditCardConfig)
        {
            string retValue = "" + random.Next(0, 10) + "" + random.Next(0, 10) + "" + random.Next(0, 10);
            return retValue;
        }
        //https://simplemaps.com/data/us-zips
        public static CityPostal generateRandomPostalCode(CreditCardConfig creditCardConfig, string country)
        {
            // TODO: get a zip code from list of zip codes not hard coded

            CityPostal retValue = new CityPostal();
            retValue.city = "Longmont";
            retValue.postal = "80503";
            retValue.state = "CO";
            retValue.stateName = "Colorado";
            return retValue;
        }

        static string ReverseString(string s)
        {
            char[] array = s.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}
