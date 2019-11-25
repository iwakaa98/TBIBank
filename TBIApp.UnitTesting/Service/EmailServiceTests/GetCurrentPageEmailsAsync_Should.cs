using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TBIApp.Data;
using TBIApp.Data.Models;
using TBIApp.Services.Mappers.Contracts;
using TBIApp.Services.Models;
using TBIApp.Services.Services;
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.EmailServiceTests
{
    [TestClass]
    public class GetCurrentPageEmailsAsync_Should
    {
        [TestMethod]
        public async Task GetCurrentPageEmailsAsync_ThrowEx_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetCurrentPageEmailsAsync_ThrowEx_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var store = new Mock<IUserStore<User>>();
            var mockManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockUser = new Mock<User>().Object;

            var statusOfEmail = EmailStatusesEnum.NotReviewed;
            var page = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, decodeService, mockLogger, mockManager, mockEncryptService);

                var result = await sut.GetCurrentPageEmailsAsync(page, statusOfEmail, mockUser);

                Assert.IsNull(result);
            };
        }

        //[TestMethod]
        //public async Task GetCurrentPageEmailsAsync_Return_CollectionOfDTO()
        //{
        //    var options = TestUtilities.GetOptions(nameof(GetCurrentPageEmailsAsync_Return_CollectionOfDTO));

        //    var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
        //    var decodeService = new Mock<IDecodeService>().Object;
        //    var mockLogger = new Mock<ILogger<EmailService>>().Object;
        //    var store = new Mock<IUserStore<User>>();
        //    var mockManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null).Object;
        //    var mockEncryptService = new Mock<IEncryptService>().Object;


        //    var mockEmail = new Mock<Email>().Object;
        //    var mockCollectionEmail = new Mock<ICollection<Email>>().Object;
        //    mockCollectionEmail.Add(mockEmail);

        //    var mockEmailDTO = new Mock<EmailDTO>().Object;
        //    var mockCollectionEmailDTO = new Mock<List<EmailDTO>>().Object;
        //    mockCollectionEmailDTO.Add(mockEmailDTO);

        //    mockEmailDTOMapper.Setup(x => x.MapFrom(mockCollectionEmail)).Returns(mockCollectionEmailDTO);


        //    var statusOfEmail = EmailStatusesEnum.NotReviewed;
        //    var mockUser = new Mock<User>().Object;
        //    var testPage = 1;

        //    var expectedResult = 2;

        //    using (var assertContext = new TBIAppDbContext(options))
        //    {

        //        assertContext.Emails.Add(new Email() { Status = statusOfEmail });
        //        assertContext.Emails.Add(new Email { Status = statusOfEmail });

        //        await assertContext.SaveChangesAsync();

        //        var emailService = new EmailService(assertContext, mockEmailDTOMapper.Object, decodeService, mockLogger, mockManager, mockEncryptService);


        //        var sut = await emailService.GetCurrentPageEmailsAsync(testPage,statusOfEmail,mockUser);

        //        Assert.AreEqual(expectedResult, sut.Count);

        //    };
        }
    }
}
