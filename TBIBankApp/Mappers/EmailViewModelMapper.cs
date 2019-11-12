using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models.Emails;

namespace TBIBankApp.Mappers
{
    public class EmailViewModelMapper : IEmailViewModelMapper
    {
        private readonly IAttachmentViewModelMapper attachmentMapper;

        public EmailViewModelMapper(IAttachmentViewModelMapper attachmentMapper)
        {
            this.attachmentMapper = attachmentMapper;
        }

        public EmailViewModel MapFrom(EmailDTO entity)
        {
            return new EmailViewModel()
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Subject = entity.Subject,
                Body = entity.Body,   
                Status = entity.Status.ToString(),
                Attachments = this.attachmentMapper.MapFrom(entity.Attachments),
                AttachmentCount = entity.Attachments.Count(),
                UserId = entity.UserId,
                User = entity.User,
                RegisteredInDataBase = entity.RegisteredInDataBase,
                LastStatusUpdate = entity.LastStatusUpdate
            };
        }

        public EmailDTO MapFrom(EmailViewModel entity)
        {
            return new EmailDTO()
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Subject = entity.Subject,
                //Status =  entity.Status,
                Attachments = this.attachmentMapper.MapFrom(entity.Attachments),
                UserId = entity.UserId,
                User = entity.User,
                RegisteredInDataBase = entity.RegisteredInDataBase,
                LastStatusUpdate = entity.LastStatusUpdate
            };
        }
        public IList<EmailViewModel> MapFrom(ICollection<EmailDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<EmailDTO> MapFrom(ICollection<EmailViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
