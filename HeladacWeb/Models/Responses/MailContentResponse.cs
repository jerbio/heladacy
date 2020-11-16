using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Responses
{
    public class MailContentResponse
    {
        public string id { get; set; }
        public string body { get; set; }
        public string htmlBody { get; set; }
    }
}
