using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class HeladacError:Exception
    {
        public HeladacErrorCode errorCode { get; set;}
    }
}
