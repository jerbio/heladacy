using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class EmailLogEntry
    {
        public bool isDeleted
        {
            get
            {
                return this.isDeleted_DB;
            }
        }
        public bool isArchived
        {
            get
            {
                return this.isArchived_DB;
            }
        }
        public Email email
        {
            get
            {
                return email_DB;
            }
        }
        public string _id = Guid.NewGuid().ToString();
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
        public string creationTime { get; set; }
        public string email_userId { get; set; }
        [ForeignKey("email_userId")]
        public Email email_DB { get; set; }
        public bool isDeleted_DB { get; set; }
        public long timeOfDeletion { get; set; }
        public bool isArchived_DB { get; set; }
        public long timeOfArchive { get; set; }
        public bool isRead { get; set; }
        [NotMapped]
        public JArray readToggleHistory {get; set;}
        public string readToggleHistory_DB { 
            get 
            {
                return this.readToggleHistory.ToString();
            }
            set 
            {
                this.readToggleHistory = JArray.Parse(value);
            } 
        }

        public JArray archiveToggleHistory { get; set; }
        public string archiveToggleHistory_DB
        {
            get
            {
                return this.archiveToggleHistory.ToString();
            }
            set
            {
                this.archiveToggleHistory = JArray.Parse(value);
            }
        }
    }
}
