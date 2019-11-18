using Microsoft.Extensions.Logging;
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
using TBIApp.Services.Services.Contracts;

namespace TBIApp.UnitTesting.Service.EmailServiceTest
{
    [TestClass]
    public class EmailService_Should
    {
        [TestMethod]
        public async Task GetEmailsPagesByTypeAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetEmailsPagesByTypeAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

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
                var sut = new EmailService(assertContext, mockEmailDTOMapper, decodeService, mockLogger, mockEncryptService);

                var result = await sut.GetEmailsPagesByTypeAsync((EmailStatusesEnum)status);

                Assert.AreEqual(1, result);
            };
        }

        [TestMethod]
        public async Task GetCurrentPageEmailsAsync_ThrowEx_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetCurrentPageEmailsAsync_ThrowEx_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var decodeService = new Mock<IDecodeService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;

            var statusOfEmail = EmailStatusesEnum.NotReviewed;
            var page = 1;
          
            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, decodeService, mockLogger, mockEncryptService);

                var result = await sut.GetCurrentPageEmailsAsync(page,statusOfEmail);

                Assert.IsNull(result);
            };
        }

        [TestMethod]
        public async Task GetAllEmailsPagesAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(GetAllEmailsPagesAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock <ILogger<EmailService>>().Object;

            var expectedCount = 1;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(new Email());
                await actContext.Emails.AddAsync(new Email());

                await actContext.SaveChangesAsync();
            }
                       
            using (var assertContext = new TBIAppDbContext(options))
            {
                var sut = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                var result = await sut.GetAllEmailsPagesAsync();

                Assert.AreEqual(expectedCount, result);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShouldGet_True));

            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>();
            var mockEmail = new Mock<Email>().Object;
            var mockEmailDTO = new Mock<EmailDTO>().Object;
            mockEmailDTOMapper.Setup(x => x.MapFrom(mockEmailDTO)).Returns(mockEmail);

            var expectedResult = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper.Object, mockDecodeService, mockLogger, mockEncryptService) ;

                var sut = await emailService.CreateAsync(mockEmailDTO);

                Assert.AreEqual(expectedResult, assertContext.Emails.Count());
            }
        }

        [TestMethod]
        public async Task IsOpenAsync_ShouldGet_True()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldGet_True));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;

            var expectedResult = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                var sut = await emailService.IsOpenAsync(testId);

                Assert.AreEqual(expectedResult, sut);
            }
        }

        [TestMethod]
        public async Task IsOpenAsync_ShouldGet_False()
        {
            var options = TestUtilities.GetOptions(nameof(IsOpenAsync_ShouldGet_False));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.IsOpne = false;

            var expectedResult = false;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                var sut = await emailService.IsOpenAsync(testId);

                Assert.AreEqual(expectedResult, sut);
            }
        }

        [TestMethod]
        public async Task LockButtonAsync_ShouldSetIsOpenToTrue()
        {
            var options = TestUtilities.GetOptions(nameof(LockButtonAsync_ShouldSetIsOpenToTrue));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.IsOpne = false;

            var expectedResult = true;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                await emailService.LockButtonAsync(testId);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.IsOpne);
            }
        }

        [TestMethod]
        public async Task UnLockButtonAsync_ShouldSetIsOpenToFalse()
        {
            var options = TestUtilities.GetOptions(nameof(UnLockButtonAsync_ShouldSetIsOpenToFalse));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.IsOpne = true;

            var expectedResult = false;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                await emailService.UnLockButtonAsync(testId);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.IsOpne);
            }
        }

        [TestMethod]
        public async Task ChangeStatusAsync_ShouldChangeStatus()
        {
            var options = TestUtilities.GetOptions(nameof(ChangeStatusAsync_ShouldChangeStatus));

            var mockEmailDTOMapper = new Mock<IEmailDTOMapper>().Object;
            var mockDecodeService = new Mock<IDecodeService>().Object;
            var mockEncryptService = new Mock<IEncryptService>().Object;
            var mockLogger = new Mock<ILogger<EmailService>>().Object;

            var testId = "testId";

            var testStatus = EmailStatusesEnum.New;

            var mockEmail = new Mock<Email>().Object;
            mockEmail.Id = testId;
            mockEmail.Status = testStatus;

            var mockUser = new Mock<User>().Object;
            var newEmailStatus = EmailStatusesEnum.Open;

            var expectedResult = newEmailStatus;

            using (var actContext = new TBIAppDbContext(options))
            {
                await actContext.Emails.AddAsync(mockEmail);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new TBIAppDbContext(options))
            {
                var emailService = new EmailService(assertContext, mockEmailDTOMapper, mockDecodeService, mockLogger, mockEncryptService);

                await emailService.ChangeStatusAsync(testId, newEmailStatus,mockUser);

                var sut = assertContext.Emails.FirstOrDefault();

                Assert.AreEqual(expectedResult, sut.Status);
            }
        }
    }
}
