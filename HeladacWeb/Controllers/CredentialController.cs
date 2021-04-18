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


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCredentials(CredentialParam credentialParam)
        {
            string url = credentialParam.fullUri;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            HeladacUser heladacUser = context.Users.Find(userId);

            CredentialService credentialService = credentialParam.getCredentialService(url);
            IQueryable<CredentialService> credentialServices;
            if (credentialService.ServiceType == CredentialServiceType.none)
            {
                credentialServices = context.CredentialServices.Where(eachCredentialService => eachCredentialService.Url == credentialService.Url);
            }
            else
            {
                credentialServices = context.CredentialServices.Where(eachCredentialService => eachCredentialService.ServiceType_DB == credentialService.ServiceType_DB);
            }

            HelmUser retValue = credentialServices.Join(context.HelmUsers,
                eachCredentialService => eachCredentialService.id,
                eachHelmUser => eachHelmUser.credentialServiceId,
                (eachCredentialService, eachHelmUser) => eachHelmUser).OrderByDescending(eachHelmUser => eachHelmUser.creationTimeMs_DB).FirstOrDefault();
            
            return Ok(retValue);
        }
    }
}
