using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Responses
{
    public class EmailResponse
    {
        public string id { get; set; }
        public string emailId { get; set; }
        public string sender { get; set; } = null;
        public long time { get; set; } = 0;
        public MailBoxAddressResponse receiver { get; set; } = null;
        public List<MailBoxAddressResponse> bcc { get; set; } = null;
        public List<MailBoxAddressResponse> cc { get; set; } = null;
        public List<MailBoxAddressResponse> to { get; set; } = null;
        public string subject { get; set; } = null;
        public MailContentResponse mailContentResponse { get; set; } = null;
    }
}
