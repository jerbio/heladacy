using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    [Table("CredentialUsers")]
    public abstract class CredentialUser
    {
        public virtual string firstName { get; set; }
        public virtual string middleName { get; set; }
        public virtual string lastName { get; set; }
        public virtual string username { get; set; }
        public virtual string passwordHash { get; set; }
        public virtual string email { get; set; }
        public virtual string phoneNumber { get; set; }
    }
}
