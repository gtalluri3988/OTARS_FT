using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.ViewModels.Operations;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IConflictService : IBaseService {

        void SendFirstReminders();

        void SendSecondReminders();

        void DefaultOpenCases();

        void AutoClose();

        void Close(long id);

        void Reject(long id);

        List<ConflictAttachmentViewModel> GetAttachments(long id);

        ConflictAttachmentViewModel GetAttachment(long attachmentId);

    }// interface
}// 
