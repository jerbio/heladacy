using System;
using System.Collections.Generic;
using System.Linq;

public class CreditCardLogic
{
    Random random = new Random();
    public CreditCardLogic()
    {
        
    }

    public string generateCreditCardNumber(CreditCardConfig config)
    {
        string retValue = null;
        if(config.prefixes.Count > 0)
        {
            List<string> prefixes = config.prefixes.ToList();
            string prefix = prefixes[random.Next(0, prefixes.Count)];
            retValue = generateRandomCCNumber(prefix, config.digitCount);
        } else
        {
            retValue = generateRandomCCNumber("", config.digitCount);
        }

        return retValue;
    }


    string generateRandomCCNumber(string prefix, int digitCount)
    {
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
    /// FOllowed directions from https://www.dcode.fr/luhn-algorithm#f1
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    int generateLUN(string digits)
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

    string ReverseString(string s)
    {
        char[] array = s.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }
}