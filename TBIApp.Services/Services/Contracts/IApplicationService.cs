using System.Threading.Tasks;
using TBIApp.Services.Models;

namespace TBIApp.Services.Services.Contracts
{
    public interface IApplicationService
    {
        Task<LoanApplicationDTO> CreateAsync(LoanApplicationDTO newLoan);
    }
}