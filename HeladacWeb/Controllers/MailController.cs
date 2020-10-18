using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeladacWeb.Models;
using HeladacWeb.Models.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace HeladacWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : HeladacBaseController
    {

        [HttpPost]
        public async Task<IActionResult> ReceiveMail (EmailParam emailParam)
        {   
            Email email = emailParam.toEmail();
            if (email.receiverMailboxAddresses.Count < 1)
            {
                return BadRequest("No email reveiver provided");
            }

            List<MailboxAddress> mailboxAddresses = email.receiverMailboxAddresses;
            DateTimeOffset currentTime = DateTimeOffset.UtcNow;
            List<MailboxAddress> invalidMailAddresses = new List<MailboxAddress>();
            List<MailboxAddress> validMailAddresses = new List<MailboxAddress>();
            foreach (MailboxAddress mailboxAddress in mailboxAddresses)
            {
                string emailAddress = mailboxAddress.Address;
                var mailSplit = emailAddress.Split('@');
                string emailUserName = mailSplit[0].ToLower();
                string domain = mailSplit[1].ToLower();
                HelmUser helmUser = await context.HelmUsers.SingleOrDefaultAsync(user => user.UserName == emailUserName).ConfigureAwait(false);
                
                if (helmUser == null)
                {
                    invalidMailAddresses.Add(mailboxAddress);
                }
                else
                {
                    validMailAddresses.Add(mailboxAddress);
                }

                var isHeladacEmail = mailboxAddress.Address.isHeladacEmail();

                if(isHeladacEmail.Item1 && isHeladacEmail.Item2)
                {
                    EmailLogEntry emailLogEntry = new EmailLogEntry()
                    {
                        isRead_DB = false,
                        isArchived_DB = false,
                        isDeleted_DB = false,
                        userId = helmUser.Id,
                        senderEmail_DB = email.sender_DB,
                        creationTime_DB = currentTime.ToUnixTimeMilliseconds(),
                        email_DB = email
                    };
                    context.Emails.Add(email);
                    context.EmailLogEntrys.Add(emailLogEntry);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

                    
            }
            
            return Ok();
        }
    }
}
