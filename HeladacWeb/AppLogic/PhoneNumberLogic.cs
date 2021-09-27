using HeladacWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Twilio;
using HeladacWeb.Data;

namespace HeladacWeb.AppLogic
{
    public class PhoneNumberLogic : AppLogic
    {
        public PhoneNumberLogic(HeladacDbContext context):base(context)
        {

        }
        public async Task<PhoneNumber> generatePhoneNumber(HeladacUser heladacUser, bool useGenerallyAllocation = true, HelmUser helmUser = null)
        {
            if (heladacUser != null)
            {
                PhoneNumber newPhoneNumber;
                if (useGenerallyAllocation)
                {
                    newPhoneNumber = heladacUser.latestPhoneNumber;    
                    if (heladacUser.latestPhoneNumber == null)
                    {
                        newPhoneNumber = getGenerallyAllocationPhoneNumber();
                    }
                    
                    heladacUser.latestPhoneNumberId = newPhoneNumber.id;
                    
                } else
                {
                    newPhoneNumber = getRemotePhoneNumber();
                    newPhoneNumber.isGeneral = false;
                    heladacUser.latestPhoneNumber = newPhoneNumber;
                    context.PhoneNumbers.Add(newPhoneNumber);
                }
                
                
                PhoneNumberList phoneNumberList = await context.PhoneNumberList.FindAsync(heladacUser.Id);
                if(phoneNumberList== null)
                {
                    phoneNumberList = new PhoneNumberList()
                    {
                        heladacUser = heladacUser,
                        numbers = new List<PhoneNumber>()
                    };
                    phoneNumberList.numbers.Add(newPhoneNumber);
                }


                if (persistAfterChanges)
                {
                    await persistDbChanges().ConfigureAwait(false);
                }
                return newPhoneNumber;
            }

            throw new ArgumentNullException("You need to provide a heladac user");
        }

        public PhoneNumber getRemotePhoneNumber()
        {
            PhoneNumber retValue = getGenerallyAllocationPhoneNumber();
            return retValue;
        }

        public PhoneNumber getGenerallyAllocationPhoneNumber()
        {
            PhoneNumber retValue = Utility._allPhoneNumbers.RandomEntry();
            return retValue;
        }

        public void populateJayNumbers()
        {
            context.PhoneNumbers.AddRange(Utility.jayNumbers);
            context.SaveChanges();
        }
    }
}
