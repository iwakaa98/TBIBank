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
        private readonly IDecodeService decodeService;

        public EmailService(TBIAppDbContext dbcontext, IEmailDTOMapper emailDTOMapper, IDecodeService decodeService)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.emailDTOMapper = emailDTOMapper ?? throw new ArgumentNullException(nameof(emailDTOMapper));
            this.decodeService = decodeService ?? throw new ArgumentNullException(nameof(decodeService));
        }

        public async Task<EmailDTO> CreateAsync(EmailDTO emailDTO)
        {
            var email = this.emailDTOMapper.MapFrom(emailDTO);

            email.RegisteredInDataBase = DateTime.Now;

            this.dbcontext.Emails.Add(email);

            await this.dbcontext.SaveChangesAsync();

            return emailDTO;
        }

        public async Task<ICollection<EmailDTO>> GetAllAsync(int page)
        {
            var emails = await this.dbcontext.Emails
                .Skip((page - 1) * 15)
                .Take(15)
                .Include(a => a.Attachments)
                .ToListAsync();

            if (emails == null) throw new ArgumentNullException("No emails found!");

            return this.emailDTOMapper.MapFrom(emails); 
        }

        public async Task<ICollection<EmailDTO>> GetCurrentPageEmails(int page, EmailStatusesEnum typeOfEmail)
        {
            
            var emails = await this.dbcontext.Emails
                .Where(e => e.Status == typeOfEmail)
                .OrderByDescending(e => e.RegisteredInDataBase)
                .Skip((page - 1) * 15)
                .Take(15)
                .Include(a=> a.Attachments)
                .Include(e => e.User)
                .ToListAsync();

            //Just for test

            var decodeEmails = emails;

            for (int i = 0; i < decodeEmails.Count; i++)
            {
                emails[i].Body = await this.decodeService.DecodeAsync(decodeEmails[i].Body);
            }

            //Just for test
            if (emails == null) throw new ArgumentNullException("No emails found!");

            return this.emailDTOMapper.MapFrom(emails);
        }

        //Replace string/int with ChangeStatusDTO model// Add User if user != Manager || Operator is diff!?
        public async Task ChangeStatus(string emailId, EmailStatusesEnum newEmaiLStatus, User currentUser)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == emailId);

            if (email == null) throw new ArgumentNullException("Email not found!");

            email.Status = newEmaiLStatus;

            email.LastStatusUpdate = DateTime.Now;

            email.UserId = currentUser.Id;

            email.IsOpne = false;

            this.dbcontext.Emails.Update(email);

            await this.dbcontext.SaveChangesAsync();

            //log.Info($"User {currentUser} changed status from {lastStatus} to {newStatus} at {DateTime.Now}")

            //log.Warning("ArgumentNullEx)
            //log.Fatal("")

        }

        public Task<int> GetEmailsPagesByType(EmailStatusesEnum statusOfEmail)
        {
            var totalEmails = this.dbcontext.Emails.Where(e => e.Status == statusOfEmail).Count();

            return Task.FromResult(totalEmails % 15 == 0 ? totalEmails / 15 : totalEmails / 15 + 1);
        }

        public Task<int> GetAllEmailsPages()
        {
            var totalEmails = this.dbcontext.Emails.Count();

            return Task.FromResult(totalEmails % 15 == 0 ? totalEmails / 15 : totalEmails / 15 + 1);
        }

        public async Task<bool> IsOpen(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);
            return email.IsOpne;
        }
        public async Task LockButton(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);
            email.IsOpne = true;

            //dbcontext.Emails.Update(email);
            await this.dbcontext.SaveChangesAsync();
        }
        public async Task UnLockButton(string id)
        {
            var email = await this.dbcontext.Emails.FirstOrDefaultAsync(e => e.Id == id);
            email.IsOpne = false;

            //dbcontext.Emails.Update(email);
            await this.dbcontext.SaveChangesAsync();
        }
    }
}
