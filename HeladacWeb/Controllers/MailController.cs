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
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HeladacWeb.Models.Responses;

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
                HelmUser helmUser = await context.HelmUsers.SingleOrDefaultAsync(user => user.username == emailUserName).ConfigureAwait(false);

                if (helmUser == null)
                {
                    invalidMailAddresses.Add(mailboxAddress);
                }
                else
                {
                    validMailAddresses.Add(mailboxAddress);
                    var isHeladacEmail = mailboxAddress.Address.isHeladacEmail();

                    if (isHeladacEmail.Item1 && isHeladacEmail.Item2)
                    {
                        EmailLogEntry emailLogEntry = new EmailLogEntry()
                        {
                            isRead_DB = false,
                            isArchived_DB = false,
                            isDeleted_DB = false,
                            userId = helmUser.id,
                            heladacUserId = helmUser.heladacUserId,
                            senderEmail_DB = email.sender_DB,
                            creationTime_DB = currentTime.ToUnixTimeMilliseconds(),
                            email_DB = email
                        };
                        context.Emails.Add(email);
                        context.EmailLogEntrys.Add(emailLogEntry);
                        await persistDbChanges().ConfigureAwait(false);
                    }
                }
            }

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MailContent([FromQuery] MailContentParam emailParam)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<EmailLogEntry> emailLogEntrys = context.EmailLogEntrys
                .Include(emailLogEntry => emailLogEntry.email_DB)
                .Include(emailLogEntry => emailLogEntry.receiver_DB)
                .Where(emailLogEntrys => emailLogEntrys.email_DB.id == emailParam.id).ToList();
            EmailLogEntry emailLogEntry = emailLogEntrys.FirstOrDefault();
            if(emailLogEntry != null)
            {
                HelmUser receiver = emailLogEntry.receiver;
                Email email = emailLogEntry.email;
                EmailResponse emailResponse = email.ToEmailResponse(receiver);
                return Ok(emailResponse);
            }
            return StatusCode(400, "Invalid id provided");
        }

        [HttpGet]
        [Authorize]
        [Route("usermails")]
        public async Task<IActionResult> UserMails([FromQuery] EmailParam emailParam)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IQueryable<EmailLogEntry> EmailLogEntryQuery = context.EmailLogEntrys;
            int pageIndex = emailParam.pageIndex < 0 ? 0 : emailParam.pageIndex;
            int beginIndex = pageIndex * emailParam.pageSize;

            var EmailLogEntries = EmailLogEntryQuery.Where(logEntry => logEntry.heladacUserId == userId)
                .OrderByDescending(logEntry => logEntry.creationTime_DB)
                .Skip((int)beginIndex).Take((int)emailParam.pageSize);
            if (emailParam.includeMailContent)
            {
                EmailLogEntries = EmailLogEntries.Include(emailLog => emailLog.email_DB.mailContent_DB);
            }

            EmailLogEntries = EmailLogEntries
                .Include(o => o.receiver_DB)
                .Include(o => o.email_DB);

            List<EmailLogEntry> emailLogEntriesList = EmailLogEntries.ToList();
            List<EmailResponse> retValue = new List<EmailResponse>();
            foreach (EmailLogEntry emailLogEntry in emailLogEntriesList)
            {
                Email eachEmail = emailLogEntry.email;
                context.Entry(eachEmail).State = EntityState.Detached;
                context.Entry(emailLogEntry).State = EntityState.Detached;
                eachEmail.bccMailboxAddresses = null;
                eachEmail.ccMailboxAddresses = null;
                eachEmail.receiverMailboxAddresses = null;
                var emailResponse = eachEmail.ToEmailResponse(emailLogEntry.receiver);
                retValue.Add(emailResponse);
            }

            return Ok(retValue);
            
        }


        [HttpGet]
        [Authorize]
        [Route("previews")]
        public async Task<IActionResult> mailPreiviews([FromQuery] EmailParam emailParam)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IQueryable<EmailLogEntry> EmailLogEntryQuery = context.EmailLogEntrys;
            int pageIndex = emailParam.pageIndex < 0 ? 0 : emailParam.pageIndex;
            int beginIndex = pageIndex * emailParam.pageSize;

            var EmailLogEntries = EmailLogEntryQuery.Where(logEntry => logEntry.heladacUserId == userId && !logEntry.isArchived_DB)
                .OrderByDescending(logEntry => logEntry.creationTime_DB)
                .Skip((int)beginIndex).Take((int)emailParam.pageSize);
            EmailLogEntries = EmailLogEntries.Include(emailLog => emailLog.email_DB.mailContent_DB);

            EmailLogEntries = EmailLogEntries
                .Include(o => o.receiver_DB)
                .Include(o => o.email_DB);

            List<EmailLogEntry> emailLogEntriesList = EmailLogEntries.ToList();
            List<EmailResponse> emailResponses = new List<EmailResponse>();
            foreach (EmailLogEntry emailLogEntry in emailLogEntriesList)
            {
                Email eachEmail = emailLogEntry.email;
                context.Entry(eachEmail).State = EntityState.Detached;
                context.Entry(emailLogEntry).State = EntityState.Detached;
                eachEmail.bccMailboxAddresses = null;
                eachEmail.ccMailboxAddresses = null;
                eachEmail.receiverMailboxAddresses = null;
                var emailResponse = eachEmail.ToEmailResponse(emailLogEntry.receiver);
                emailResponses.Add(emailResponse);
            }

            int currentIndex = pageIndex + EmailLogEntries.Count();
            PaginatedResponse<EmailResponse> retValue = new PaginatedResponse<EmailResponse>()
            {
                index = currentIndex,
                data = emailResponses
            };

            return Ok(retValue);

        }
    }
}
