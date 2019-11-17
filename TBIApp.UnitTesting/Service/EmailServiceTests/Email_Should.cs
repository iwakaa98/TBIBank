using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.EmailServiceTest
{
    [TestClass]
    public class Email_Should
    {
        [TestMethod]
        public async Task GetEmailsPagesByTypeAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            
            var status = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(
                    new Email
                    {
                        Status = (EmailStatusesEnum)status
                    });

                await actContext.SaveChangesAsync();
            }

            using(var assertContext = new TBIAppDbContext(options))
            { 
                var sut = new EmailService(assertContext, mockEmailDTOMapper, decodeService);

                var result = await sut.GetEmailsPagesByTypeAsync((EmailStatusesEnum)status);

                Assert.AreEqual(1, result);
            };
        }

        [TestMethod]
        public async Task GetAllEmailsPagesAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllEmailsPagesAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;

            var expectedCount = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(new Email());
                await actContext.Emails.AddAsync(new Email());

                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            { 
                var sut = new EmailService(assertContext, mockEmailDTOMapper, decodeService);

                var result = await sut.GetAllEmailsPagesAsync();

                Assert.AreEqual(expectedCount, result);
            }

        }
    }
}
