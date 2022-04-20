using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
//using MimeKit.;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class Email
    {
        protected string _id = Guid.NewGuid().ToString();

        public string id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }
        [NotMapped]
        protected virtual List<MailboxAddress> _cc { get; set; } = null;
        public string sender
        {
            get
            {
                return this.sender_DB;
            }
        }

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
        [NotMapped]
        protected virtual List<MailboxAddress> _receiver { get; set; } = null;
        [NotMapped]
        public List<MailboxAddress> receiverMailboxAddresses
        {
            get
            {
                return (_receiver ?? (_receiver = new List<MailboxAddress>()));
            }
            set
            {
                _receiver = value;
            }
        }
        [NotMapped]
        public virtual string receiver
        {
            get
            {
                return receiver_DB;
            }
        }
        #region dbEntries
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
                    string address = (string)(MailboxAddressJson.GetValue("Address"));
                    string name = (string)(MailboxAddressJson.GetValue("Name"));
                    int encodingCodePage = (int)MailboxAddressJson.SelectToken("Encoding.CodePage");
                    JArray routeObj = (JArray)(MailboxAddressJson.SelectToken("Route"));
                    List<string> route = new List<string>();
                    foreach( JObject eachRoute in routeObj)
                    {
                        route.Add((string)eachRoute);
                    }

                    //string[] route = (string[])routeObj;
                    Encoding encoding = Encoding.GetEncoding(encodingCodePage);
                    MailboxAddress mailboxAddress = new MailboxAddress(encoding, name, route, address);
                    //var MailboxAddress = JsonConvert.DeserializeObject<MailboxAddress>(MailboxAddressJson.ToString());
                    receivers.Add(mailboxAddress);
                }
            }
        }
        public MailContent mailContent { 
            get { return mailContent_DB; }
        }
        public virtual string mailContentId {get;set;}
        [ForeignKey("mailContentId")]
        public MailContent mailContent_DB
        {
            get;set;
        }
        #endregion
    }
}
