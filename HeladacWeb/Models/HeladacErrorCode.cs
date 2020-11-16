using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public enum HeladacErrorCode
    {
        No_Error,
        Email_Id_Missing
    }
    static class HeladacErrorConfig {
        

        public static readonly Dictionary<HeladacErrorCode, string> ErrorCodeMessgae = new Dictionary<HeladacErrorCode, string>() {
            { HeladacErrorCode.No_Error, "No error" },
            { HeladacErrorCode.Email_Id_Missing, "Email Id missing in request" }
        };
    }

    


}
