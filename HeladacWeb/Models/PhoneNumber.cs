using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class PhoneNumber
    {
        protected string _id = Guid.NewGuid().ToString();
        protected virtual PhoneNumberSource _source { get; set; }
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
        public virtual bool isGeneral { get; set; } = false;
        public virtual bool isVerifiedActive { get; set; }
        public virtual string fullNumber { get; set; }
        public virtual string thirdPartyId { get; set; }
        public virtual PhoneNumberSource source { get {
                return _source;
            }
        }

        public void updatePhoneNumberSource(PhoneNumberSource source)
        {
            this._source = source;
        }
        public string source_DB
        {
            get
            {
                return _source.ToString().ToLower();
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    _source = PhoneNumberSource.none;
                }
                else
                {
                    _source = Utility.ParseEnum<PhoneNumberSource>(value);
                }
            }
        }
    }
}
