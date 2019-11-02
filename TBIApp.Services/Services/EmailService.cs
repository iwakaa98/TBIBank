using System;
using System.Collections.Generic;
using System.Text;
using TBIApp.Data;

namespace TBIApp.Services.Services
{
    public class EmailService
    {
        private readonly TBIAppDbContext dbcontext;

        public EmailService(TBIAppDbContext dbcontext)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        //public async Task<EmailDTO> CreateAsync(EmailDTO emailDTO)
        //{
        //    var email = this.emailMapper.MapFrom(emailDTO);

        //    //TODO 
        //    email.Status = new EmailStatus { StatusName = "Not reviwed" };

        //    this.dbcontext.Emails.Add(email);
        //    await this.dbcontext.SaveChangesAsync();

        //    return this.emailMapper.MapFrom(email);
        //}

        //public async Task<ICollection<EmailDTO>> GetAllAsync()
        //{
        //    var emails = await this.dbcontext.Emails.Select(x => x).ToListAsync();

        //    return this.emailMapper.MapFrom(emails);
        //}

    }
}
