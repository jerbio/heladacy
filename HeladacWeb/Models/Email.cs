using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
//using MimeKit.;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class Email
    {
        public string _id = Guid.NewGuid().ToString();
        public string id {
            get {
                return _id;
            }
            set {
                _id = value;
            } 
        }

        public string emailId_DB { get; set; }
        public string subJect_DB { get; set; }
        public DateTimeOffset timeOfCreation { 
            get 
            { 
                return DateTimeOffset.FromUnixTimeMilliseconds(this.timeOfCreationMs);
            } 
        }
        public long timeOfCreationMs { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        [Required]
        public virtual string sender_DB { get; set; }
        public virtual string sender_Name_DB { get; set; }
        public virtual string sender_Email_DB { get; set; }

        [NotMapped]
        protected virtual List<MailboxAddress> _bcc { get; set; } = null;
        [NotMapped]
        public List<MailboxAddress> bccMailboxAddresses
        {
            get
            {
                return (_bcc ?? (_bcc = new List<MailboxAddress>()));
            }
            set
            {
                _bcc = value;
            }
        }
        [Required]
        public virtual string bcc_DB
        {
            get
            {
                JArray retValue = new JArray();
                foreach (var MailboxAddress in bccMailboxAddresses)
                {
                    JObject MailboxAddressJson = JObject.FromObject(MailboxAddress);
                    retValue.Add(MailboxAddressJson);
                }

                return retValue.ToString();
            }
            set
            {
                var MailboxAddressesJarray = JArray.Parse(value);
                List<MailboxAddress> bccs = this.bccMailboxAddresses;
                foreach (JObject MailboxAddressJson in MailboxAddressesJarray)
                {
                    var MailboxAddress = JsonConvert.DeserializeObject<MailboxAddress>(MailboxAddressJson.ToString());
                    bccs.Add(MailboxAddress);
                }
            }
        }
        [NotMapped]
        protected virtual List<MailboxAddress> _cc { get; set; } = null;
        [NotMapped]
        public List<MailboxAddress> ccMailboxAddresses
        {
            get
            {
                return (_cc ?? (_cc = new List<MailboxAddress>()));
            }
            set
            {
                _cc = value;
            }
        }
        [Required]
        public virtual string cc_DB
        {
            get
            {
                JArray retValue = new JArray();
                foreach (var MailboxAddress in ccMailboxAddresses)
                {
                    JObject MailboxAddressJson = JObject.FromObject(MailboxAddress);
                    retValue.Add(MailboxAddressJson);
                }

                return retValue.ToString();
            }
            set
            {
                var MailboxAddressesJarray = JArray.Parse(value);
                List<MailboxAddress> ccs = this.ccMailboxAddresses;
                foreach (JObject MailboxAddressJson in MailboxAddressesJarray)
                {
                    var MailboxAddress = JsonConvert.DeserializeObject<MailboxAddress>(MailboxAddressJson.ToString());
                    ccs.Add(MailboxAddress);
                }
            }
        }
        [NotMapped]
        protected virtual List<MailboxAddress> _receiver { get; set; } = null;
        [NotMapped]
        public List<MailboxAddress> receiverMailboxAddresses { 
            get
            {
                return (_receiver ?? (_receiver = new List<MailboxAddress>()));
            }
            set
            {
                _receiver = value;
            }
        }
        [Required]
        public virtual string receiver_DB { 
            get 
            {
                JArray retValue = new JArray();
                foreach (var MailboxAddress in receiverMailboxAddresses)
                {
                    JObject MailboxAddressJson = JObject.FromObject(MailboxAddress);
                    retValue.Add(MailboxAddressJson);
                }

                return retValue.ToString();
            }
            set {
                var MailboxAddressesJarray = JArray.Parse(value);
                List<MailboxAddress> receivers = this.receiverMailboxAddresses;
                foreach (JObject MailboxAddressJson in MailboxAddressesJarray)
                {
                    var MailboxAddress = JsonConvert.DeserializeObject<MailboxAddress>(MailboxAddressJson.ToString());
                    receivers.Add(MailboxAddress);
                }
            } 
        }
        public virtual string content_DB { get; set; }
        public virtual string content_html_DB { get; set; }
    }
}
