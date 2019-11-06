using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly IEmailDTOMapper emailDTOMapper;

        public EmailService(TBIAppDbContext dbcontext, IEmailDTOMapper emailDTOMapper)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.emailDTOMapper = emailDTOMapper ?? throw new ArgumentNullException(nameof(emailDTOMapper));
        }

        public async Task<EmailDTO> CreateAsync(EmailDTO emailDTO)
        {
            var email = this.emailDTOMapper.MapFrom(emailDTO);

            email.RegisteredInDataBase = DateTime.Now;

            //TODO remove from here
            email.Status = EmailStatusesEnum.NotReviewed;

            this.dbcontext.Emails.Add(email);
            await this.dbcontext.SaveChangesAsync();

            return emailDTO;
        }

        public async Task<ICollection<EmailDTO>> GetAllAsync()
        {
            var emails = await this.dbcontext.Emails
                .Take(6)
                .Include(a => a.Attachments)
                .ToListAsync();

            if (emails == null) throw new ArgumentNullException("No emails found!");

            return this.emailDTOMapper.MapFrom(emails); 
        }

        public async Task<ICollection<EmailDTO>> GetCurrentPageEmails(int page, EmailStatusesEnum typeOfEmail)
        {
            
            var emails = await this.dbcontext.Emails
                .Where(e => e.Status == typeOfEmail)
                .Skip((page - 1) * 15)
                .Take(15)
                .Include(a=> a.Attachments)
                .ToListAsync();

            if (emails == null) throw new ArgumentNullException("No emails found!");

            return this.emailDTOMapper.MapFrom(emails);


        }

        public int GetEmailsPagesByType(EmailStatusesEnum statusOfEmail)
        {
            var result = this.dbcontext.Emails
                .Where(e => e.Status == statusOfEmail)
                .Count();

            if (result % 6 == 0)
                return result / 6;
            else
                return result / 6 + 1;



        }
    }
}
