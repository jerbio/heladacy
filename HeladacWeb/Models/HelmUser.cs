using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    [Table("HelmUsers")]
    public class HelmUser: IdentityUser
    {
        public string address1 { get; }
        public string address2 { get; }
        public string city { get; }
        public string state { get; }
        public string country { get; }
        public string postal { get; }
        public HeladacUser heladacUser { get; }
        [Required]
        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser_db { get; set; }

        public string address1_DB { get; set; }
        public string address2_DB { get; set; }
        public string city_DB { get; set; }
        public string state_DB { get; set; }
        public string country_DB { get; set; }
        public string postal_DB { get; set; }
    }
}
