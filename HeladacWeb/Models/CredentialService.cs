using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public abstract class CredentialService
    {
        [NotMapped]
        public virtual CredentialServiceType ServiceType  { get; set; }
        public virtual string ServiceType_DB {
            get
            {
                return ServiceType.ToString().ToLower();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    ServiceType = CredentialServiceType.none;
                }
                else
                {
                    ServiceType = Utility.ParseEnum<CredentialServiceType>(value);
                }
            }
        }




        static public CredentialService createCredentialServiceByUrl(string url)
        {
            if(url.isNot_NullEmptyOrWhiteSpace())
            {
                CredentialService retValue = null;
                string lowerCaseString = url.ToLower();

                bool isNetflixUrl = NetflixCredentialService.isNetflixUrl(url);
                if(isNetflixUrl)
                {
                    retValue = new NetflixCredentialService();
                } else
                {
                    retValue = new NoneCredentialService();
                }




                return retValue;
            }
            throw new ArgumentNullException("url", "The url cannot be null or empty");
        }
    }
}
