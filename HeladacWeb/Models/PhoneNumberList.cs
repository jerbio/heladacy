using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class PhoneNumberList
    {
        [Key]
        public string heladacUserId { get; set; }
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser { get; set; }
        public ICollection<PhoneNumber> numbers { get; set; }
    }
}
