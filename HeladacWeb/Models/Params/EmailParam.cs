using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public string bodyString { get; set; }
        public string addresses { get; set; }
        public string sendType { get; set; }
        public string id { get; set; }
        public bool includeMailContent { get; set; } = false;
        public int pageIndex { get; set; } = Utility.defaultPageIndex;
        public int pageSize { get; set; } = Utility.defaultPageSize;
        public byte [] attachment { get; set; }
        public Email toEmail()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(this.content);
            MemoryStream stream = new MemoryStream(byteArray);
            var message = MimeMessage.Load(stream);
            
            Email retValue = new Email()
            {
                emailId_DB = message.MessageId,
                subJect_DB = message.Subject,
                receiverMailboxAddresses = message.To.Select(mailBoxAddress => (MailboxAddress)mailBoxAddress).ToList(),
                bccMailboxAddresses= message.Bcc.Select(mailBoxAddress => (MailboxAddress)mailBoxAddress).ToList(),
                ccMailboxAddresses = message.Cc.Select(mailBoxAddress => (MailboxAddress)mailBoxAddress).ToList(),
                mailContent_DB = new MailContent()
                {
                    content_DB = message.TextBody,
                    content_html_DB = message.HtmlBody,
                    raw = this.content
                },
                sender_DB = ((MailboxAddress)message.From.FirstOrDefault()).Address,
                
            };

            return retValue;
        }
    }
}
