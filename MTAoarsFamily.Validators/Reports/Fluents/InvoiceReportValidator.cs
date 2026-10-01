using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using FluentValidation;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.Validators.Shared.Fluent;
using MTAoarsFamily.ViewModels.Interfaces;
using MTAoarsFamily.ViewModels.Reports;

namespace MTAoarsFamily.Validators.Operations.Fluents {
    
    [FluentRegisterable]
    public class InvoiceReportValidator : AbstractValidator<InvoiceReportViewModel> {

        public InvoiceReportValidator() {
            RuleFor(x => x.Year).GreaterThan(0);
            RuleFor(x => x.Month).GreaterThan(0);
        }

    }// class
}// namespace
