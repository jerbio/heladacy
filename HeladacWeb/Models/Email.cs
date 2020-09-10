using System;
using System.Collections.Generic;
using System.Linq;
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
        public string subJect_DB { get; set; }
        public DateTimeOffset timeOfCreation { 
            get 
            { 
                return DateTimeOffset.FromUnixTimeMilliseconds(this.timeOfCreationMs);
            } 
        }
        public long timeOfCreationMs { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        public string sender_DB { get; set; }
        public string bcc_DB { get; set; }
        public string cc_DB { get; set; }
        public string receiver_DB { get; set; }
        public string content_DB { get; set; }
    }
}
