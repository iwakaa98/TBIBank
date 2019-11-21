using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly ILoanApplicationDTOMapper loanApplicatioDTOMapper;

        public ApplicationService(TBIAppDbContext dbcontext, ILoanApplicationDTOMapper loanApplicatioDTOMapper)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            this.loanApplicatioDTOMapper = loanApplicatioDTOMapper ?? throw new ArgumentNullException(nameof(loanApplicatioDTOMapper));
        }

        public async Task<LoanApplicationDTO> CreateAsync(LoanApplicationDTO newLoan)
        {
            var loanApplication = this.loanApplicatioDTOMapper.MapFrom(newLoan);

            if (loanApplication == null) throw new ArgumentNullException();

            loanApplication.Status = LoanApplicationStatus.NotReviewed;

            this.dbcontext.LoanApplications.Add(loanApplication);

            await this.dbcontext.SaveChangesAsync();

            return newLoan;
        }

        public async Task RemoveAsync(string id)
        {
            var application = await this.dbcontext.LoanApplications.FirstOrDefaultAsync(x => x.EmailId == id);

            this.dbcontext.LoanApplications.Remove(application);

            await this.dbcontext.SaveChangesAsync();
        }
    }
}
