using System.Threading.Tasks;

namespace TBIApp.Services.Services.Contracts
{
    public interface IGmailAPIService
    {
        Task SyncEmails();
    }
}