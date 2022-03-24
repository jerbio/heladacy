using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models.Params
{
    public class MailContentParam
    {
        public string id { get; set; }
        public bool rawFormat { get; set; } = true;
    }
}
