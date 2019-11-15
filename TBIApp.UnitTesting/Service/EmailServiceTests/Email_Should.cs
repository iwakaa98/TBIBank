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
        public async Task GetEmailsPagesByTypeAsync_Test()
        {


            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_Test));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            
            var status = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                var email = await actContext.Emails.AddAsync(
                    new Email
                    {
                        Status = (EmailStatusesEnum)status
                    });

                await actContext.SaveChangesAsync();

                var sut = new EmailService(actContext, mockEmailDTOMapper, decodeService);

                var result = await sut.GetEmailsPagesByTypeAsync((EmailStatusesEnum)status);

                Assert.AreEqual(1, result);
            };

           

        }
    }
}
