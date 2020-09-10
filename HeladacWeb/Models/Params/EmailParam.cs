using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Params
{
    public class EmailParam
    {
        public string senderEmail { get; set; }
        public string receiverEmail { get; set; }
        public string bccEnail { get; set; }
        public string ccEnail { get; set; }
        public string content { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string addresses { get; set; }
        public string sendType { get; set; }
        public byte [] attachment { get; set; }
        public Email toEmail()
        {
            Email retValue = new Email()
            {
                sender_DB = this.senderEmail,
                subJect_DB = this.subject,
                bcc_DB = this.bccEnail,
                cc_DB = this.ccEnail,
                content_DB = this.content,
                receiver_DB = this.receiverEmail
            };

            return retValue;
        }
    }
}
