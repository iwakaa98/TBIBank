using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.Services.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly TBIAppDbContext dbcontext;
        private readonly IReportDiagramDTOMapper reportDigramMapper;

        public StatisticsService(TBIAppDbContext dbcontext, IReportDiagramDTOMapper reportDigramMapper)
        {

            this.dbcontext = dbcontext;
            this.reportDigramMapper = reportDigramMapper;
        }

        public async Task<ReportDiagramDTO> GetStatisticsAsync()
        {
            var totalcount = await this.dbcontext.Emails.CountAsync();
            var repDiagram = new ReportDiagram
            {
                InvalidCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.InvalidApplication).Count(),
                //PercentInvalid = Math.Round(((double)(this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.InvalidApplication).Count()) / totalcount * 100), 2),
                NotReviewedCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.NotReviewed).Count(),
                //PercentNotReviewed = Math.Round(((double)(this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.NotReviewed).Count()) / totalcount * 100),2),
                NewCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.New).Count(),
                //PercentNew = Math.Round(((double)(this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.New).Count()) / totalcount * 100), 2),
                OpenCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Open).Count(),
                //PercentOpen = Math.Round(((double)(this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Open).Count()) / totalcount * 100), 2),
                ClosedCount = this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Closed).Count(),
                //PercentClosed = Math.Round(((double)(this.dbcontext.Emails.Where(e => e.Status == EmailStatusesEnum.Closed).Count()) / totalcount * 100), 2),
                RejectedCount = this.dbcontext.LoanApplications.Where(a => a.Status == LoanApplicationStatus.Rejected).Count(),
                //PercentRejected = Math.Round(((double)(this.dbcontext.LoanApplications.Where(e => e.Status == LoanApplicationStatus.Rejected).Count()) / totalcount * 100), 2),
                AcceptedCount = this.dbcontext.LoanApplications.Where(a => a.Status == LoanApplicationStatus.Accepted).Count(),
                //PercentAccepted = Math.Round(((double)(this.dbcontext.LoanApplications.Where(e => e.Status == LoanApplicationStatus.Accepted).Count()) / totalcount * 100), 2)

            };

            return this.reportDigramMapper.MapFrom(repDiagram);
        }
    }
}
