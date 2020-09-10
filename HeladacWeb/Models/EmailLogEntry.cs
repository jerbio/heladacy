using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Models
{
    public class EmailLogEntry
    {

        bool _isRead { get; set; } = false;
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


        #region dbEntries
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
        public long creationTime_DB { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        [Required]
        public string userId { get; set; }
        [ForeignKey("userId")]
        public HelmUser receiver_DB { get; set; }
        [Required]
        public string senderEmail_DB { get; set; }
        [Required]
        public string emailId { get; set; }
        [ForeignKey("emailId")]
        public Email email_DB { get; set; }
        public bool isDeleted_DB { get; set; } = false;
        public long timeOfDeletionMs_DB { get; set; }
        public bool isArchived_DB { get; set; } = false;
        public long timeOfLastArchiveMs_DB { get; set; }
        public bool isRead_DB { 
            get { 
                return _isRead; 
            } 
            set {
                _isRead = value;
            } 
        }
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
        [NotMapped]
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
        #endregion
    }
}
