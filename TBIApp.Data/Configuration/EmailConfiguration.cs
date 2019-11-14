using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TBIApp.Data.Models;

namespace TBIApp.Data.Configuration
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(a => a.Attachments)
                .WithOne(e => e.Email);

            //builder.HasOne<LoanApplication>()
            //    .WithOne(e => e.EmailId)
            //    .HasForeignKey<LoanApplication>(a => a.Id);
        
        }
    }
}

