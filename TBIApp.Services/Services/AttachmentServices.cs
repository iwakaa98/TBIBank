using System;
using System.Collections.Generic;
using System.Text;
using TBIApp.Data;

namespace TBIApp.Services.Services
{
    public class AttachmentService
    {
        private readonly TBIAppDbContext dbcontext;
        //private readonly IAttachmentDTOMapper attachmentDTOMapper;

        //public AttachmentService(TBIAppDbContext dbcontext) //IAttachmentDTOMapper attachmentDTOMapper)
        //{
        //    this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        //    this.attachmentDTOMapper = attachmentDTOMapper ?? throw new ArgumentNullException(nameof(dbcontext));
        //}

        //public async Task<AttachmentDTO> CreateAsync(AttachmentDTO attachmentDTO)
        //{
        //    var attachment = this.attachmentDTOMapper.MapFrom(attachmentDTO);

        //    this.dbcontext.Attachments.Add(attachment);
        //    await this.dbcontext.SaveChangesAsync();

        //    return this.attachmentDTOMapper.MapFrom(attachment);
        //}
    }
}
