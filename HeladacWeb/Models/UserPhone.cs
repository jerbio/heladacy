using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class UserPhone
    {
        protected string _id = Guid.NewGuid().ToString();
        protected string _helmUserId { get; set; } = "";
        public string id
        {
            get
            {
                return _helmUserId;
            }
            set
            {
                _helmUserId = value;
            }
        }
        [Required]
        public string helmUserId
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
        [ForeignKey("helmUserId")]
        public HelmUser helmUser { get; set; }
        /// <summary>
        /// When a phone number is associated with a specific helm user
        /// </summary>
        public long creationTime { get; set; }
        /// <summary>
        /// When a phone number is no longer valid for the helm user
        /// </summary>
        public long expiryTime { get; set; }
        public bool isDefunct { get; set; } = false;
    }
}
