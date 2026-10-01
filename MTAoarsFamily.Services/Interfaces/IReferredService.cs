using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.ViewModels.Maintenance;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IReferredService : IBaseService {

        long AddHeader(long companyId);

        long AddDetail(ReferredDetailViewModel model);

        void SubmitHeader(long headerId);

        void Approve(long detailId, string comments);

        ReferredHeaderViewModel GetHeader(long headerId);

        ReferredDetailViewModel GetDetail(long detailId);

        ReferredAttachmentViewModel GetAttachment(long attachmentId);

    }// interface
}// 
