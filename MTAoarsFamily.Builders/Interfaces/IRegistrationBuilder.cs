using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.DomainModels;
using System.IO;

namespace MTAoarsFamily.Builders.Interfaces {
    public interface IRegistrationBuilder {

        RegistrationStartViewModel GetStartViewModel();

        RegistrationIndexViewModel GetIndexViewModel(long companyId, long agencyTypeId);

        RegistrationIndexViewModel GetIndexViewModel();
               
        T GetNew<T>(RegistrationIndexViewModel model) where T : RegistrationViewModel;

         RegistrationInclusionViewModel GetInclusion(RegistrationIndexViewModel model);

        RegistrationConflictViewModel GetConflict(RegistrationIndexViewModel model);

        RegistrationReinstationViewModel GetReinstation(RegistrationIndexViewModel model);
        List<RegistrationResponseViewModel> GetRegistrations(Stream stream);

    }// interface
}// namespace
