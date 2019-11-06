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

        public EmailViewModel MapFrom(EmailDTO entity)
        {
            return new EmailViewModel()
            {
                Id = entity.Id,
                Sender = entity.Sender,
                Subject = entity.Subject,
                Status = entity.Status,
                //Attachments = this.attachmentDTOMapper.MapFrom(entity.Attachments),
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
                Status =  entity.Status,
                //Attachments = this.attachmentDTOMapper.MapFrom(entity.Attachments),
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
