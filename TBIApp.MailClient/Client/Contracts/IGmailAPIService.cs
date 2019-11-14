using System.Threading.Tasks;

namespace TBIApp.MailClient.Client.Contracts
{
    public interface IGmailAPIService
    {
        Task SyncEmails();
    }
}
