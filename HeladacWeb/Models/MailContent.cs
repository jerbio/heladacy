using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class MailContent
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

        public virtual string content_DB { get; set; }
        public virtual string content_html_DB { get; set; }
        public virtual string raw { get; set; }
    }
}
