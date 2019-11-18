using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TBIApp.MailClient.Client.Contracts;

namespace TBIBankApp.Infrastructure.HostedServices
{
    public class HostedGetEmailsService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public HostedGetEmailsService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.timer = new Timer(GetEmails, null, TimeSpan.FromSeconds(0),
               TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private async void GetEmails(object state)
        {
            using (var scope = this.serviceProvider.CreateScope())
            {
                var gmailApiService = scope.ServiceProvider.GetRequiredService<IGmailAPIService>();

                await gmailApiService.SyncEmails();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
