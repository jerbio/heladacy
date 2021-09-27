﻿using HeladacWeb.Data;
using HeladacWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.AppLogic
{
    public class HelmUserLogic : AppLogic
    {
        private static readonly HeladacWeb.Services.EncryptionService encryptionService = new Services.EncryptionService();
        public HelmUserLogic(HeladacDbContext context):base(context)
        {

        }

        public async Task<HelmUser> generateHelmUser(HeladacUser heladacUser, CredentialService credentialService, bool createNewPhoneNumber = true)
        {
            HelmUser retValue = new HelmUser();
            retValue.heladacUser_db = heladacUser;
            string password = Utility.generateHelmPassword();
            retValue.firstName = Utility.personNameGenerator.GenerateRandomFirstName();
            retValue.lastName = Utility.personNameGenerator.GenerateRandomLastName();
            string encryptedPassword = encryptionService.Encrypt(password);
            retValue.passwordHash = encryptedPassword;
            var emailAndUserName = Utility.generateHelmEmail();
            retValue.username = emailAndUserName.Item2;
            retValue.email = emailAndUserName.Item1;
            retValue.credentialService_DB = credentialService;
            if(heladacUser.latestPhoneNumber!=null)
            {
                retValue.phoneNumber = heladacUser.latestPhoneNumber;
            }

            if(createNewPhoneNumber)
            {
                PhoneNumberLogic phoneNumberLogic = new (context);
                phoneNumberLogic.persistAfterChanges = false;
                PhoneNumber phoneNumber = await phoneNumberLogic.generatePhoneNumber(heladacUser).ConfigureAwait(false);
                //heladacUser.latestPhoneNumber = phoneNumber;
                //retValue.phoneNumber = phoneNumber;
            }

            context.HelmUsers.Add(retValue);
            if (persistAfterChanges)
            {
                await persistDbChanges().ConfigureAwait(false);
            }
            
            return retValue;
        }
    }
}