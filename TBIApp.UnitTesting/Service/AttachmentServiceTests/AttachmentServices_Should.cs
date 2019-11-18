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

namespace TBIApp.UnitTesting.Service.AttachmentServiceTests
{
    [TestClass]
    public class AttachmentServices_Should
    {
        [TestMethod]
        public async Task CreateAsync_ShoulCreate()
        {
            var options = TestUtilities.GetOptions(nameof(CreateAsync_ShoulCreate));

            var mockAttachmentDTO = new Mock<AttachmentDTO>().Object;
            var mockAttachment = new Mock<Attachment>().Object;
            var mockAttachmentDTOMapper = new Mock<IAttachmentDTOMapper>();
            mockAttachmentDTOMapper.Setup(a => a.MapFrom(mockAttachmentDTO)).Returns(mockAttachment);

            var expectedResult = 1;

            using (var assertContext = new TBIAppDbContext(options))
            {
                var attachmentService = new AttachmentService(assertContext, mockAttachmentDTOMapper.Object);

                var sut = await attachmentService.CreateAsync(mockAttachmentDTO);

                Assert.AreEqual(expectedResult, assertContext.Attachments.Count());

            };
        }
    }
}
