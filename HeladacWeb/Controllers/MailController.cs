using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeladacWeb.Models;
using HeladacWeb.Models.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
            if (!email.receiver_DB.isNot_NullEmptyOrWhiteSpace())
            {
                return BadRequest("No email reveiver provided");
            }
            string emailAddress = email.receiver_DB;
            string emailUserName = emailAddress.Split('@')[0].ToLower();
            HelmUser helmUser = await context.HelmUsers.FindAsync(emailUserName).ConfigureAwait(false);
            if(helmUser == null)
            {
                return NotFound("User email not found");
            }
            EmailLogEntry emailLogEntry = new EmailLogEntry()
            {
                isRead_DB = false,
                isArchived_DB = false,
                isDeleted_DB = false,
                userId = helmUser.Id,
                senderEmail_DB = email.sender_DB,
                creationTime_DB = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                email_DB = email
            };
            context.Emails.Add(email);
            context.EmailLogEntrys.Add(emailLogEntry);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Ok();
        }
    }
}
