using System.Collections.Generic;
using TBIApp.Services.Models;
using TBIBankApp.Models;

namespace TBIBankApp.Mappers.Contracts
{
    public interface IReportDiagramViewModelMapper
    {
        IList<ReportDiagramViewModel> MapFrom(ICollection<ReportDiagramDTO> entities);
        IList<ReportDiagramDTO> MapFrom(ICollection<ReportDiagramViewModel> entities);
        ReportDiagramViewModel MapFrom(ReportDiagramDTO entity);
        ReportDiagramDTO MapFrom(ReportDiagramViewModel entity);
    }
}