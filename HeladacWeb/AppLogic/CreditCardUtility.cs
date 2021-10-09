using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.AppLogic
{
    public static class CreditCardUtility
    {
        public static readonly ImmutableList<CreditCardConfig> creditCardConfigs = ImmutableList.Create<CreditCardConfig>(
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.amex,
                digitCount = 15,
                prefixes = new List<string>() { "34", "37" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.diners,
                digitCount = 15,
                prefixes = new List<string>() { "300", "301", "302", "303", "36", "38" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.discover,
                digitCount = 16,
                prefixes = new List<string>() { "6011" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.enroute,
                digitCount = 16,
                prefixes = new List<string>() { "2014", "2149" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.jcb15,
                digitCount = 16,
                prefixes = new List<string>() { "2100", "1800" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.jcb16,
                digitCount = 16,
                prefixes = new List<string>() { "3088", "3096", "3112", "3158", "3337", "3528" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.mastercard,
                digitCount = 16,
                prefixes = new List<string>() { "51", "52", "53", "54", "55" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.mastercard,
                digitCount = 16,
                prefixes = new List<string>() { "51", "52", "53", "54", "55" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.visa,
                digitCount = 13,
                prefixes = new List<string>() { "4539", "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.visa,
                digitCount = 16,
                prefixes = new List<string>() { "4539", "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.voyager,
                digitCount = 13,
                prefixes = new List<string>() { "8699" }
            },
            new CreditCardConfig()
            {
                creditCardType = CreditCardType.voyager,
                digitCount = 16,
                prefixes = new List<string>() { "8699" }
            }
        );
    }
}
