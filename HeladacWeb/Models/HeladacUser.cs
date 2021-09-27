using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class HeladacUser : IdentityUser
    {
        public string latestPhoneNumberId { get; set; }
        [ForeignKey("latestPhoneNumberId")]
        public PhoneNumber latestPhoneNumber { get; set; }
    }
}
