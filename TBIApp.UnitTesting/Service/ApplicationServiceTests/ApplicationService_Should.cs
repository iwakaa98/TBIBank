using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services;

namespace TBIApp.UnitTesting.Service.ApplicationServiceTests
{
    [TestClass]
    public class ApplicationService_Should
    {
        [TestMethod]
        public async Task CreateAsync_ShouldCreate()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldCreate));

            var mockApplication = new Mock<LoanApplication>().Object;
            var mockApplicationDTO = new Mock<LoanApplicationDTO>().Object;
            var mockLoanApplicationDTOMapper = new Mock<ILoanApplicationDTOMapper>();
            mockLoanApplicationDTOMapper.Setup(am => am.MapFrom(mockApplicationDTO)).Returns(mockApplication);

            var expected = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var applicationService = new ApplicationService(assertContext, mockLoanApplicationDTOMapper.Object);

                var sut = await applicationService.CreateAsync(mockApplicationDTO);
                var count = assertContext.LoanApplications.Count();

                Assert.AreEqual(expected, count);
            }
        }
    }
}
