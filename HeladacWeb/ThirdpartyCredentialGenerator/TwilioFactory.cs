using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Twilio;

namespace HeladacWeb.ThirdpartyCredentialGenerator
{
    public class TwilioFactory
    {
        static bool isInitialize = false;
        public void initializeTwilioClient()
        {
            if(!isInitialize)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                string authToken = configuration["MessagingService:twilio:authToken"];
                string accountSid = configuration["MessagingService:twilio:accountSid"];
                TwilioClient.Init(accountSid, authToken);
                isInitialize = true;
            }
        }

        
    }
}
