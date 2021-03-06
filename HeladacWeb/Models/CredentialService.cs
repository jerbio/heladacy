﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class CredentialService
    {
        protected string _id = Guid.NewGuid().ToString();
        protected string _Domain = "";
        protected string _Url = "";
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

        public virtual string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }

        public virtual string Domain_DB
        {
            get
            {
                return _Domain;
            }
            set
            {
                _Domain = value;
            }
        }


        public virtual string Url_DB
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
            }
        }

        public string id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        static public CredentialService createCredentialServiceByUrl(string url, string domain)
        {
            if(url.isNot_NullEmptyOrWhiteSpace())
            {
                Uri urlUri = null;

                try
                {
                    urlUri = new Uri(url);
                }
                catch (Exception exception)
                {
                    throw new Exception("Invalid url provided");
                }
                CredentialService retValue = null;
                string lowerCaseString = url.ToLower();

                bool isNetflixUrl = NetflixCredentialService.isNetflixUrl(urlUri.AbsoluteUri);
                if(isNetflixUrl)
                {
                    retValue = new NetflixCredentialService();
                } else
                {
                    retValue = new NoneCredentialService();
                }

                retValue.Url = url;
                Uri domainUri = null;
                if(domain.isNot_NullEmptyOrWhiteSpace())
                {
                    try
                    {
                        domainUri = new Uri(domain);
                    }
                    catch(Exception exception)
                    {
                        try
                        {
                            domainUri = new Uri(url);
                        }
                        catch (Exception domainException)
                        {
                            throw new Exception("Invalid domain url provided");
                        }
                    }
                } else
                {
                    retValue._Domain = urlUri.Host;
                }

                if(domainUri!=null)
                {
                    retValue._Domain = domainUri.Host;
                }
                
                return retValue;
            }
            throw new ArgumentNullException("url", "The url cannot be null or empty");
        }
    }
}
