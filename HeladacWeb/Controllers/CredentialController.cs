using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HeladacWeb.Models;
using HeladacWeb.Models.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeladacWeb.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class CredentialController : HeladacBaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCredentials(CredentialParam credentialParam)
        {
            string url = credentialParam.fullUri;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            HeladacUser heladacUser = context.Users.Find(userId);
            HelmUser helmUser = HelmUser.generateHelmUser(heladacUser, credentialParam.getCredentialService(url));
            context.HelmUsers.Add(helmUser);
            await context.SaveChangesAsync().ConfigureAwait(false);
            return Ok(helmUser);
        }
    }
}
