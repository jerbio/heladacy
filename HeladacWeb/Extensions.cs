using HeladacWeb.Models;
using HeladacWeb.Models.Responses;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb
{
    public static class Extensions
    {
        public static MailContentResponse ToMailContentResponse(this MailContent mailContent)
        {
            MailContentResponse retValue = new MailContentResponse() {
                id = mailContent.id,
                body = mailContent.content_DB,
                htmlBody = mailContent.content_html_DB
            };
            return retValue;
        }

        public static MailBoxAddressResponse ToMailAddressBoxResponse(this MailboxAddress mailboxAddress)
        {
            MailBoxAddressResponse retValue = new MailBoxAddressResponse() { address = mailboxAddress.Address, name = mailboxAddress.Name };
            return retValue;
        }

        public static EmailResponse ToEmailResponse(this Email email, HelmUser user)
        {
            EmailResponse retValue = new EmailResponse() {
                id = email.id,
                bcc = email.bccMailboxAddresses?.Select(mailAddress => mailAddress.ToMailAddressBoxResponse()).ToList(),
                cc = email.ccMailboxAddresses?.Select(mailAddress => mailAddress.ToMailAddressBoxResponse()).ToList(),
                to = email.receiverMailboxAddresses?.Select(mailAddress => mailAddress.ToMailAddressBoxResponse()).ToList(),
                sender = email.sender_DB,
                emailId = email.emailId_DB,
                subject = email.subJect_DB,
                receiver = new MailBoxAddressResponse() { 
                    address = user.email,
                    name = user.fullName
                },
                
                mailContentResponse = email.mailContent_DB?.ToMailContentResponse()
            };
            return retValue;
        }
    }
}
