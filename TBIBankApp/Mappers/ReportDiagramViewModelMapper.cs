using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBIApp.Services.Models;
using TBIBankApp.Mappers.Contracts;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers
{
    public class ReportDiagramViewModelMapper : IReportDiagramViewModelMapper
    {
        public ReportDiagramViewModel MapFrom(ReportDiagramDTO entity)
        {
            return new ReportDiagramViewModel()
            {
                AcceptedCount = entity.AcceptedCount,
                NewCount = entity.NewCount,
                ClosedCount = entity.ClosedCount,
                InvalidCount = entity.InvalidCount,
                OpenCount = entity.OpenCount,
                NotReviewedCount = entity.NotReviewedCount,
                RejectedCount = entity.RejectedCount,
                PercentAccepted = entity.PercentAccepted,
                PercentClosed = entity.PercentClosed,
                PercentInvalid = entity.PercentInvalid,
                PercentNew = entity.PercentNew,
                PercentNotReviewed = entity.PercentNotReviewed,
                PercentOpen = entity.PercentOpen,
                PercentRejected = entity.PercentRejected

            };
        }
        public ReportDiagramDTO MapFrom(ReportDiagramViewModel entity)
        {
            return new ReportDiagramDTO()
            {
                AcceptedCount = entity.AcceptedCount,
                NewCount = entity.NewCount,
                ClosedCount = entity.ClosedCount,
                InvalidCount = entity.InvalidCount,
                OpenCount = entity.OpenCount,
                NotReviewedCount = entity.NotReviewedCount,
                RejectedCount = entity.RejectedCount,
                PercentAccepted = entity.PercentAccepted,
                PercentClosed = entity.PercentClosed,
                PercentInvalid = entity.PercentInvalid,
                PercentNew = entity.PercentNew,
                PercentNotReviewed = entity.PercentNotReviewed,
                PercentOpen = entity.PercentOpen,
                PercentRejected = entity.PercentRejected

            };
        }
        public IList<ReportDiagramViewModel> MapFrom(ICollection<ReportDiagramDTO> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }

        public IList<ReportDiagramDTO> MapFrom(ICollection<ReportDiagramViewModel> entities)
        {
            return entities.Select(this.MapFrom).ToList();
        }
    }
}
