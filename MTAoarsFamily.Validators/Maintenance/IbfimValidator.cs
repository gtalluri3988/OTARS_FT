using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.ViewModels.Maintenance;
using System.Text.RegularExpressions;

namespace MTAoarsFamily.Validators.Maintenance {
    public class IbfimValidator : IValidator<IbfimExcelViewModel> {
        
        public IEnumerable<ValidationMessage> Validate(IbfimExcelViewModel item) {
            if(String.IsNullOrEmpty(item.ExaminationTypeCode) ||
                !Regex.IsMatch(item.ExaminationTypeCode, "[1-3]{1,2}")) {
                item.AddError("Examination type code is invalid");
            }
            if(String.IsNullOrEmpty(item.ICNO)) {
                item.AddError("IC Number is required");
            }
            if(String.IsNullOrEmpty(item.Result) || 
                !Regex.IsMatch(item.Result, "^(Pass|Fail|Absent)$")) {
                item.AddError("Invalid result it should be either Pass or Fail or Absent");
            }
            yield break;
        }

    }// class
}// namespace
