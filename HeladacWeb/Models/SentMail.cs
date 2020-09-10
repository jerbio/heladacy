using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    [Table("SentMails")]
    public class SentMail:Email
    {
        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HelmUser sender_DB { get; set; }
    }
}
