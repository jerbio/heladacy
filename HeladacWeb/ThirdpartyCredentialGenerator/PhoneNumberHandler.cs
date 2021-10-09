using HeladacWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Api.V2010.Account.AvailablePhoneNumberCountry;

namespace HeladacWeb.ThirdpartyCredentialGenerator
{
    public class PhoneNumberHandler
    {
        public PhoneNumberHandler()
        {

        }

        public PhoneNumber purchaseNumber(string countryCode = "US")
        {
            var mobile = MobileResource.Read(pathCountryCode: countryCode, limit: 20);

            if (mobile.Count()> 0)
            {
                var firstNumber = mobile.First();
                string selectedNumber = firstNumber.FriendlyName.ToString();
                var incomingPhoneNumber = IncomingPhoneNumberResource.Create(
                    phoneNumber: new Twilio.Types.PhoneNumber(selectedNumber)
                );

                PhoneNumber retValue = new PhoneNumber()
                {
                    thirdPartyId = incomingPhoneNumber.Sid,
                    fullNumber = incomingPhoneNumber.FriendlyName,
                    isActive = true,                    
                };
                retValue.updatePhoneNumberSource(PhoneNumberSource.twilio);

                Console.WriteLine("Generated number :"+ selectedNumber+ "\nSID:"+ incomingPhoneNumber.Sid);
                return retValue;
            }

            return null;
            
        }
    }
}
