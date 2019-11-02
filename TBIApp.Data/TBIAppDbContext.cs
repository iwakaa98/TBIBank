using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TBIApp.Data.Models;

namespace TBIApp.Data
{
    public class TBIAppDbContext : IdentityDbContext<User>
    {
        public TBIAppDbContext(DbContextOptions<TBIAppDbContext> options)
          : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailStatus> EmailStatuses { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanApplicationStatus> LoanApplicationStatuses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
