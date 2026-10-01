using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.ViewModels.Maintenance;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IUploadService : IBaseService {

        void UploadIbfim(ExcelList<IbfimExcelViewModel> items);

        void UploadRegistration(ExcelList<RegistrationExcelViewModel> items);

        string GetHistoryErrorPath(long id);

        string GetHistorySuccessPath(long id);

        string GetHistoryRawPath(long id);

    }// interface
}// 
