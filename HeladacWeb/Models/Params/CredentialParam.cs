using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Params
{
    public class CredentialParam
    {
        public string domain { get; set; }
        public string fullUri { get; set; }

        public CredentialService getCredentialService(string url)
        {
            CredentialService credentialService = CredentialService.createCredentialServiceByUrl(url);
            return credentialService;
        }
    }
}
