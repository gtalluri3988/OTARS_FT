using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Maintenance;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.ViewModels.Administrative;

namespace MTAoarsFamily.Services.Interfaces
{
    public interface IAdministrativeService : IBaseService
    {
        void UpdateAgency(AdministrativeAgentViewModel model, long principalId, bool isTerminated);
        AdministrativeAgentViewModel GetByPrincipal(long id);
        AdministrativeAgentViewModel GetByPrincipalHistory(long id);
        void InsertTBEResultToAuditTrail(TBEEnquirySearchViewModel model, string TableName);
        void InsertReferredToAuditTrail(ReferredSearchViewModel model);
        void InsertRenewalToAuditTrail(RenewalDetailsViewModel model);
    }// interface
}
