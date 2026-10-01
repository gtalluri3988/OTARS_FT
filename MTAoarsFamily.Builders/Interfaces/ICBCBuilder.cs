using MTAoarsFamily.ViewModels.Operations;
using System.Collections.Generic;
using System;

namespace MTAoarsFamily.Builders.Interfaces {
    public interface ICBCBuilder : IBaseBuilder {

        CBCStartViewModel GetStartViewModel();

        CBCDetailViewModel Search(string agencyNumber, long companyId);

    }// interface
}// namespace
