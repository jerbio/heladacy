﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HeladacWeb.AppLogic;
using HeladacWeb.Models;
using HeladacWeb.Models.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeladacWeb.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class CredentialController : HeladacBaseController
    {
        HelmUserLogic helmUserLogic;
        public CredentialController():base()
        {
            helmUserLogic = new HelmUserLogic(this.context);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCredentials(CredentialParam credentialParam)
        {
            string url = credentialParam.fullUri;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            HeladacUser heladacUser = context.Users
                .Include(user => user.latestPhoneNumber)
                .Where(user => user.Id == userId).SingleOrDefault();
            CredentialService generatedCredentialService = credentialParam.getCredentialService(url, credentialParam.domain);
            
            CredentialService credentialService = generatedCredentialService;
            CredentialService retrievedCredentialService = context.CredentialServices.SingleOrDefault(eachCredentialService => eachCredentialService.Domain_DB == generatedCredentialService.Domain_DB);
            
            if(retrievedCredentialService != null)
            {
                credentialService = retrievedCredentialService;
            }

            HelmUser helmUser = await helmUserLogic.generateHelmUser(heladacUser, credentialService).ConfigureAwait(false);
            return Ok(helmUser);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCredentials([FromQuery] CredentialParam credentialParam)
        {
            if(Utility.getLastTimePhoneNumberList().isBeginningOfTime())
            {
                await Utility.updateCachedPhoneNumbers(this.context).ConfigureAwait(false);
            }

            string url = credentialParam.fullUri;
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            HeladacUser heladacUser = context.Users
                .Include(user => user.latestPhoneNumber)
                .Where(user => user.Id == userId).SingleOrDefault();

            CredentialService credentialService = credentialParam.getCredentialService(url, credentialParam.domain);
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
                (eachCredentialService, eachHelmUser) => eachHelmUser).OrderByDescending(eachHelmUser => eachHelmUser.creationTimeMs_DB).Include(helmUser => helmUser.phoneNumber).Include(helmUser => helmUser.creditCard_DB).FirstOrDefault();
            
            
            if(retValue!=null)
            {
                if(!retValue.email.isNot_NullEmptyOrWhiteSpace())
                {
                    context.Entry(retValue).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                    string domain = Utility.helmDomains[0];
                    retValue.email = retValue.username + "@" + domain;
                }
            }
            return Ok(retValue);
        }
    }
}
