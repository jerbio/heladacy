using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class NoneCredentialService : CredentialService
    {
        public override CredentialServiceType ServiceType { get; set; } = CredentialServiceType.none;
    }
}
