using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;

namespace TBIApp.Services.Mappers
{
    public class LoanApplicationDTOMapper : ILoanApplicationDTOMapper
    {
        public LoanApplicationDTO MapFrom(LoanApplication entity)
        {
            return new LoanApplicationDTO()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Body = entity.Body,
                Status = entity.Status,
                CardId = entity.Id,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,
                Email = entity.Email
            };
        }
        public LoanApplication MapFrom(LoanApplicationDTO entity)
        {
            return new LoanApplication()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EGN = entity.EGN,
                Body = entity.Body,
                Status = entity.Status,
                CardId = entity.Id,
                PhoneNumber = entity.PhoneNumber,
                EmailId = entity.EmailId,
                Email = entity.Email

            };
        }
        public IList<LoanApplicationDTO> MapFrom(ICollection<LoanApplication> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<LoanApplication> MapFrom(ICollection<LoanApplicationDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }


    }
}
