using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class PhoneNumber
    {
        protected string _id = Guid.NewGuid().ToString();
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
        /// <summary>
        /// When the phone number was added to heladac collection of phone numbers
        /// </summary>
        public long creationTime { get; set; }

        public bool isActive { get; set; }
        public bool isGeneral { get; set; } = false;
        public bool isVerifiedActive { get; set; }
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
        public string extension { get; set; }
        public string fullNumber { get; set; }
    }
}
