using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    /// <summary>
    /// Class holds the genereated credential for a user
    /// </summary>
    [Table("HelmUsers")]
    public class HelmUser: CredentialUser
    {
        private static readonly HeladacWeb.Services.EncryptionService encryptionService = new Services.EncryptionService();
        private static Dictionary<CredentialServiceType, TimeSpan> credentialTypeTODefaultExpiration = new Dictionary<CredentialServiceType, TimeSpan>() {
            { CredentialServiceType.netflix, TimeSpan.FromDays(30) },
            { CredentialServiceType.none, TimeSpan.FromDays(-1) }
        };
        [NotMapped]
        private DateTimeOffset _creationTime { get; set; } = DateTimeOffset.UtcNow;

        [NotMapped]
        private DateTimeOffset _expirationTime { get; set; } = new DateTimeOffset();

        public string fullName { 
            get {
                string retValue = (this.firstName ?? "") + " " + this.lastName ?? "";
                retValue = retValue.Trim();
                return retValue;
            } 
        }

        public string address1 { 
            get {
                return address1_DB;
            } 
        }
        public string address2
        {
            get
            {
                return address2_DB;
            }
        }
        public string city
        {
            get
            {
                return city_DB;
            }
        }
        public string state
        {
            get
            {
                return state_DB;
            }
        }
        public string country
        {
            get
            {
                return country_DB;
            }
        }
        public string postal
        {
            get
            {
                return postal_DB;
            }
        }


        public DateTimeOffset creationTime
        {
            get
            {
                return _creationTime;
            }
        }

        public DateTimeOffset expirationTime
        {
            get
            {
                return _expirationTime;
            }
        }



        public bool isDeleted_DB { get; set; }
        public long creationTimeMs_DB 
        {
            get {
                return _creationTime.ToUnixTimeMilliseconds();
            } 
            set {
                _creationTime = DateTimeOffset.FromUnixTimeMilliseconds(value);
            }
        }
        public long expirationTimeMs_DB
        {
            get
            {
                return _expirationTime.ToUnixTimeMilliseconds();
            }
            set
            {
                _expirationTime = DateTimeOffset.FromUnixTimeMilliseconds(value);
            }
        }
        public bool isActive { get; set; }
        public string decryptedPasssword { 
            get
            {
                return encryptionService.Decrypt(this.passwordHash);
            } 
        }
        protected string _id = Guid.NewGuid().ToString();
        [Key]
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
        public HeladacUser heladacUser { get; }
        [Required]
        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser_db { get; set; }
        public string credentialServiceId { get; set; }
        [ForeignKey("credentialServiceId")]
        public CredentialService credentialService_DB { get; set; }

        public string address1_DB { get; set; }
        public string address2_DB { get; set; }
        public string city_DB { get; set; }
        public string state_DB { get; set; }
        public string country_DB { get; set; }
        public string postal_DB { get; set; }
    }
}
