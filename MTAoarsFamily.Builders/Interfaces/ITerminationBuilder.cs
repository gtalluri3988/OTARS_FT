using MTAoarsFamily.ViewModels.Operations;
using System.Collections.Generic;
using System.IO;

namespace MTAoarsFamily.Builders.Interfaces {
    public interface ITerminationBuilder : IBaseBuilder {
        
        TerminationSearchResponseViewModel Search(string agencyNumber);
        IEnumerable< TerminationSearchResponseViewModel> Search(Stream stream);
        TerminationSearchResponseViewModel SearchRecovery(string agencyNumber);
        IEnumerable<TerminationSearchResponseViewModel> SearchRecovery(Stream stream);

    }// interface
}// namespace
