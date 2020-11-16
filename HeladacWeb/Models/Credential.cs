using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using HeladacWeb.Services;

namespace HeladacWeb.Models
{
    public class Credential
    {
        public string _id = Guid.NewGuid().ToString();
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

        public string credentialId { get; set; }
        [ForeignKey("credentialId")]
        public HelmUser credentialUser_DB { get; set; }

        public string helmUserId { get; set; }
        [ForeignKey("helmUserId")]
        public HelmUser helmUser_DB { get; set; }

        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HelmUser heladacUser_DB { get; set; }

        [NotMapped]
        public CredentialService credentialService { get; set; } = CredentialService.none;
        public string credentialService_DB
        {
            get
            {
                return credentialService.ToString().ToLower();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    credentialService = CredentialService.none;
                }
                else
                {
                    credentialService = Utility.ParseEnum<CredentialService>(value);
                }
            }
        }
    }
}
