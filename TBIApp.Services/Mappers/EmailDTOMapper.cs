using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class EmailDTOMapper : IEmailDTOMapper
    {
        private readonly IAttachmentDTOMapper attachmentDTOMapper;

        public EmailDTOMapper(IAttachmentDTOMapper attachmentDTOMapper)
        {
            this.attachmentDTOMapper = attachmentDTOMapper;
        }

        public Email MapFrom(EmailDTO entity)
        {
            return new Email()
            {
                RecievingDateAtMailServer = entity.Date,
                Sender = entity.Sender,
                Subject = entity.Subject,
                StatusId = entity.StatusId,
                Status = entity.Status,
                Attachments = this.attachmentDTOMapper.MapFrom(entity.Attachments),
                Body = entity.Body

            };
        }
        public EmailDTO MapFrom(Email entity)
        {
            return new EmailDTO()
            {
                Date = entity.RecievingDateAtMailServer,
                Sender = entity.Sender,
                Subject = entity.Subject,
                StatusId = entity.StatusId,
                Status = entity.Status,
                Attachments = this.attachmentDTOMapper.MapFrom(entity.Attachments),
                Body = entity.Body
            };
        }
        public IList<Email> MapFrom(ICollection<EmailDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<EmailDTO> MapFrom(ICollection<Email> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }


    }
}
