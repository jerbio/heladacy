using HeladacWeb.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Data
{
    public class HeladacDbContext : ApiAuthorizationDbContext<HeladacUser>
    {
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmailLogEntry> EmailLogEntrys { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<HeladacUser> HeladacUsers { get; set; }
        public virtual DbSet<HelmUser> HelmUsers { get; set; }
        public virtual DbSet<SentMail> SentMails { get; set; }
        public virtual DbSet<CredentialService> CredentialServices { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<PhoneNumberList> PhoneNumberList { get; set; }

        public HeladacDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EmailLogEntry>()
                .HasIndex(emailEntry => new { emailEntry.userId, emailEntry.emailId })
                .IsUnique();

            modelBuilder.Entity<EmailLogEntry>()
                .HasKey(emailEntry => new { emailEntry.id })
                .IsClustered(false);

            modelBuilder.Entity<EmailLogEntry>()
                .HasIndex(emailEntry => new { emailEntry.userId, emailEntry.id})
                .HasName("UserId_Email")
                .IsUnique()
                .IsClustered(true);

            modelBuilder.Entity<EmailLogEntry>()
                .HasIndex(emailEntry => new { emailEntry.userId, emailEntry.creationTime_DB, emailEntry.id })
                .HasName("UserId_CreationTime_Email")
                .IsUnique();

            modelBuilder.Entity<EmailLogEntry>()
                .HasIndex(emailEntry => new { emailEntry.userId, emailEntry.isRead_DB, emailEntry.creationTime_DB, emailEntry.id })
                .HasName("UserId_IsRead_CreationTime_Email")
                .IsUnique();

            modelBuilder.Entity<HelmUser>()
                .HasKey(helmUser => new { helmUser.id })
                .IsClustered(false);

            modelBuilder.Entity<HelmUser>()
                .HasIndex(helmUser => new { helmUser.heladacUserId, helmUser.id })
                .HasName("HelmUser_HeladacUser")
                .IsUnique()
                .IsClustered(true);

            modelBuilder.Entity<Email>()
                .HasIndex(emailEntry => new { emailEntry.emailId_DB})
                .HasName("Email_EmailId")
                .IsUnique();

            modelBuilder.Entity<CredentialService>()
                .HasIndex(credentialService => new { credentialService.Domain_DB })
                .HasName("CredentialService_Domain")
                .IsUnique();

            modelBuilder.Entity<CredentialService>()
                .HasIndex(credentialService => new { credentialService.Domain_DB })
                .HasName("CredentialService_Domain")
                .IsUnique();

            modelBuilder.Entity<PhoneNumber>()
                .HasIndex(phoneNumber => new { phoneNumber.fullNumber})
                .HasName("PhoneNumber")
                .IsUnique();

            modelBuilder.Entity<PhoneMessage>()
                .HasIndex(phoneMessage => new { phoneMessage.thirdPartyId })
                .HasName("PhoneNumber")
                .IsUnique();

        }
    }
}
