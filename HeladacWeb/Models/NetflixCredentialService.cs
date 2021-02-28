using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class NetflixCredentialService:CredentialService
    {
        public override CredentialServiceType ServiceType { get;  set; } = CredentialServiceType.netflix;

        static public bool isNetflixUrl(string url)
        {
            string lowerCaseString = url.ToLower();
            bool retValue = lowerCaseString.Contains("netflix");
            return retValue;
        }
    }
}
