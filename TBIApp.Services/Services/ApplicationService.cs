using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly LoanApplicationDTOMapper loanApplicatioDTOMapper;

        public ApplicationService(TBIAppDbContext dbcontext, LoanApplicationDTOMapper loanApplicatioDTOMapper)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.loanApplicatioDTOMapper = loanApplicatioDTOMapper ?? throw new ArgumentNullException(nameof(loanApplicatioDTOMapper));
        }

        public async Task<LoanApplicationDTO> CreateAsync(LoanApplicationDTO newLoan)
        {
            var loanApplication = this.loanApplicatioDTOMapper.MapFrom(newLoan);

            if (loanApplication == null) throw new ArgumentNullException();

            this.dbcontext.LoanApplications.Add(loanApplication);

            await this.dbcontext.SaveChangesAsync();

            return newLoan;
        }

    }
}
