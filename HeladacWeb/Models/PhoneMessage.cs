using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class PhoneMessage
    {
        protected string _id = Guid.NewGuid().ToString();
        public string source { get; set; }
        public string destination { get; set; }
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
        [Required]
        public string helmUserId
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
        [ForeignKey("helmUserId")]
        public HelmUser helmUser { get; set; }

        [Required]
        public string heladacUserId
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
        [ForeignKey("heladacUserId")]
        public HeladacUser heladacUser { get; set; }
        [Required]
        public long timeOfMessage { get; set; }
    }
}
