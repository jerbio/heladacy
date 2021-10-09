using HeladacWeb.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace HeladacWeb.Controllers
{
    public class PhoneController : TwilioController
    {
        protected HeladacDbContext context = new HeladacDBContextFactory().CreateDbContext(new string[0]);
        public TwiMLResult Index(SmsRequest incomingMessage)
        {

            var messagingResponse = new MessagingResponse();
            messagingResponse.Message("The copy cat says: " +
                                      incomingMessage.Body);

            return TwiML(messagingResponse);
        }
    }
}
