using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class PhoneMessage
    {
        protected string _id = Guid.NewGuid().ToString();
        protected string _sourcePhoneNumberId { get; set; }
        protected string _destinationPhoneNumberId { get; set; }
        protected string _thirdPartyId = "";
        protected string _raw = "";
        public string notRegisteredSourceNumber { set; get; }
        public string notRegisteredDestinationNumber { set; get; }


        [NotMapped]
        public string sourceNumber {
            get {
                string retValue = null;
                if(notRegisteredDestinationNumber.isNot_NullEmptyOrWhiteSpace())
                {
                    retValue = notRegisteredDestinationNumber;
                    return retValue;
                }

                if(sourcePhoneNumberId.isNot_NullEmptyOrWhiteSpace() && sourcePhoneNumber!=null)
                {
                    retValue = sourcePhoneNumber.fullNumber;
                }
                return retValue;
            }
        }
        [NotMapped]
        public string destinationNumber
        {
            get
            {
                string retValue = null;
                if (notRegisteredDestinationNumber.isNot_NullEmptyOrWhiteSpace())
                {
                    retValue = notRegisteredDestinationNumber;
                    return retValue;
                }

                if (destinationPhoneNumberId.isNot_NullEmptyOrWhiteSpace() && destinationPhoneNumber != null)
                {
                    retValue = destinationPhoneNumber.fullNumber;
                }
                return retValue;
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

        [Required]
        public string heladacUserId
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
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser { get; set; }
        [Required]
        public long timeOfMessage { get; set; }

        public string sourcePhoneNumberId
        {
            get
            {
                return _sourcePhoneNumberId;
            }
            set
            {
                _sourcePhoneNumberId = value;
            }
        }
        [ForeignKey("sourcePhoneNumberId")]
        public PhoneNumber sourcePhoneNumber { get; set; }

        public string destinationPhoneNumberId
        {
            get
            {
                return _destinationPhoneNumberId;
            }
            set
            {
                _destinationPhoneNumberId = value;
            }
        }
        [ForeignKey("destinationPhoneNumberId")]
        public virtual PhoneNumber destinationPhoneNumber { get; set; }
        public virtual string raw
        {
            get { return _raw; }
            set { _raw = value; }
        }

        public virtual string thirdPartyId
        {
            get { return _thirdPartyId; }
            set { _thirdPartyId = value; }
        }
    }
}
