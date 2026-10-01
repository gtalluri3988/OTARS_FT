using System.Linq;
using MTAoarsFamily.Builders.Interfaces;
using MTAoarsFamily.Utilities.IoC;
using MTAoarsFamily.ViewModels.Operations;
using MTAoarsFamily.ViewModels.Interfaces;
using AutoMapper;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Mvc;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.DomainModels;
using System;
using MTAoarsFamily.Utilities.Interfaces;
using System.IO;
using System.Collections.Generic;
using MTAoarsFamily.Utilities.CSV;
using Microsoft.Practices.Unity;
using MTAoarsFamily.ViewModels.Shared;
using System.Reflection;

namespace MTAoarsFamily.Builders.Operations
{
    public class RegistrationBuilder : IRegistrationBuilder
    {
        IObjectCreator viewModelCreator;
        ILookupRepository lookupRepository;
        IMemberRepository memberRepository;

        IScopeDataProvider dataProvider;
        IAgencyRepository agencyRepository;

        public RegistrationBuilder(IObjectCreator viewModelCreator, ILookupRepository lookupRepository, IMemberRepository memberRepository, IScopeDataProvider dataProvider, IAgencyRepository agencyRepository)
        {
            this.viewModelCreator = viewModelCreator;
            this.lookupRepository = lookupRepository;
            this.memberRepository = memberRepository;
            this.dataProvider = dataProvider;
            this.agencyRepository = agencyRepository;
        }

        public RegistrationStartViewModel GetStartViewModel()
        {
            var model = viewModelCreator.Create<RegistrationStartViewModel>();
            var identity = dataProvider.Get<Identity>(GlobalConstants.CurrentIdentity);
            if (identity.IsMTA == false)
            {
                model.Company = new LookupItem { ID = identity.CompanyID, Code = identity.CompanyCode, Description = identity.CompanyName };
            }
            return model;
        }

        public RegistrationIndexViewModel GetIndexViewModel(long companyId, long agencyTypeId)
        {
            var model = viewModelCreator.Create<RegistrationIndexViewModel>();
            var agencyType = lookupRepository.Get<LookupAgencyType>(agencyTypeId);
            model.CompanyID = companyId;
            model.AgencyType = Mapper.Map<LookupItem>(agencyType);

            SetGeneralFamily(ref model);

            return model;
        }

        public void SetGeneralFamily<T>(ref T viewModel)
        {
            var isGeneralProp = typeof(T).GetProperty("IsGeneral");
            if (isGeneralProp != null) isGeneralProp.SetValue(viewModel, false, null);
            var isFamilyProp = typeof(T).GetProperty("IsFamily");
            if (isFamilyProp != null) isFamilyProp.SetValue(viewModel, true, null);
        }

        public RegistrationIndexViewModel GetIndexViewModel()
        {
            var model = viewModelCreator.Create<RegistrationIndexViewModel>();
            return model;
        }




        public T GetNew<T>(RegistrationIndexViewModel model) where T : RegistrationViewModel
        {
            var output = viewModelCreator.Create<T>();
            PopulateLookups(output);
            output.Agency = Mapper.Map(model, output.Agency);
            var agencies = memberRepository.GetActiveAgencies(model.ICNumber);
            if (agencies.Count() > 0)
            {
                output.Address = Mapper.Map<AddressViewModel>(agencies.First().Address);
            }
            var cn = memberRepository.Get(model.ICNumber);
            if (cn == null)
            {
                output.CorporateNominee = Mapper.Map(model, output.CorporateNominee);

                // 20200811 - Add to cater TO register agent as Police hit error
                // ======================================================================
                if (string.IsNullOrEmpty(output.CorporateNominee.NewICNumber))
                {
                    if (model.ICType.Code == LookupConstants.ICTypes.PoliceArmyPassport && !string.IsNullOrEmpty(model.ICNumber))
                        output.CorporateNominee.NewICNumber = model.ICNumber;
                }
            }
            else
            {
                output.CorporateNominee = Mapper.Map<CorporateNomineeViewModel>(cn);
                if (agencies.Count() == 0)
                {
                    output.CorporateNominee.IsBancaStaff = model.IsBancaStaff;
                }
            }
            output.CorporateNominee.TbeCategory = model.TbeCategory;
            output.Agency.M2Exam = model.M2Exam;
            output.Agency.DateOfExam = model.DateOfExam;
            // output.Agency.NumberofYears = model.NumberofYears;
            output.Agency.JoinYear = model.JoinYear;
            output.Agency.JoinMonth = model.JoinMonth;
            output.Agency.MTAAwards = model.MTAAwards;
            output.Agency.SocialMediaAddress = model.SocialMediaAddress;

            output.CorporateNominee.Level = model.Level;
            if (typeof(T) != typeof(IndividualRegistrationViewModel)) output.CorporateNominee.Spouse = null;
            output.AgencyType = model.AgencyType;
            output.CompanyID = model.CompanyID;
            //var tbeCategory = lookupService.GetTbeCategories().FirstOrDefault(m => m.ID == model.TbeCategoryID);
            //output.CorporateNominee.Qualification.IsTBEExempted = tbeCategory.Code == LookupConstants.TbeCategory.Exempted;
            output.Agency.ShouldDisplayAuthorizedCapital = (output as IExistableBoardMembers) != null;
            return output;
        }

        public void PopulateLookups(RegistrationViewModel model)
        {
            //model.CorporateNominee.Qualification.EducationalQualifications = lookupService.GetEducationalQualifications();
            //model.CorporateNominee.Qualification.InsuranceQualifications = lookupService.GetInsuranceQualifications();
            PopulateAgencyBankerLookups(model as IExistableAgencyBanker);
            // model.Guarantor.GuarantorTypes = lookupService.GetGuarantorTypes();
            //  model.Address.States = lookupService.GetStates();
            model.ICTypes = lookupRepository.GetAll<LookupICType>().Cast<ILookupEntity>().ToLookupItem();
        }

        public RegistrationInclusionViewModel GetInclusion(RegistrationIndexViewModel model)
        {
            var agency = memberRepository.GetActiveAgencies(model.ICNumber).First();
            var output = Mapper.Map<RegistrationInclusionViewModel>(model);
            output.Agency = Mapper.Map<AgencyDisplayViewModel>(agency);
            output.AgencyID = agency.ID;
            output.CompanyID = model.CompanyID;
            //   output.Guarantor.GuarantorTypes = lookupService.GetGuarantorTypes();
            return output;
        }

        public RegistrationConflictViewModel GetConflict(RegistrationIndexViewModel model)
        {
            var agency = memberRepository.GetActiveAgencies(model.ICNumber).First();
            var output = Mapper.Map<RegistrationConflictViewModel>(model);
            output.Agency = Mapper.Map<AgencyDisplayViewModel>(agency);
            output.AgencyID = agency.ID;
            output.Principals = output.Agency.AgencyPrincipals.Select(s => new LookupItem()
            {
                ID = s.CompanyID,
                Code = s.CompanyCode,
                Description = String.Format("{0}({1})", s.CompanyName, s.AgencyNumber)
            });
            output.CompanyID = model.CompanyID;
            // output.Guarantor.GuarantorTypes = lookupService.GetGuarantorTypes();
            return output;
        }

        public RegistrationReinstationViewModel GetReinstation(RegistrationIndexViewModel model)
        {
            // var agency = memberRepository.GetAgencies(model.ICNumber).First();
            var agency = agencyRepository.GetReinstateHistory(model.ICNumber, model.CompanyID, GetFamilyIntermediaryTypeId(), model.AgencyType.ID).Agency;
            var output = Mapper.Map<RegistrationReinstationViewModel>(model);
            output.Agency = Mapper.Map<AgencyDisplayViewModel>(agency);
            output.AgencyID = agency.ID;
            output.CompanyID = model.CompanyID;
            return output;
        }
        //Commected un used code during agency type migration

        //public RegistrationViewModel GetViewModel(Agency agency) {
        //    RegistrationViewModel model = null;

        //    switch (agency.LookupAgencyType.Code) {

        //        case LookupConstants.AgencyType.Individual:
        //            model = Mapper.Map<IndividualRegistrationViewModel>(agency);
        //            break;
        //        case LookupConstants.AgencyType.Partnership:
        //            model = Mapper.Map<PartnershipRegistrationViewModel>(agency);
        //            break;
        //        case LookupConstants.AgencyType.PrivateLimitedCompany:
        //        case LookupConstants.AgencyType.PublicLimitedCompany:
        //        case LookupConstants.AgencyType.Cooperative:
        //        case LookupConstants.AgencyType.GovernmentAgency:
        //            model = Mapper.Map<CorporateRegistrationViewModel>(agency);
        //            break;
        //        case LookupConstants.AgencyType.SoleProprietorship:
        //            model = Mapper.Map<SoleProprietorshipRegistrationViewModel>(agency);
        //            break;
        //        default:

        //            break;
        //    }

        //    return model;

        //}

        void PopulateAgencyBankerLookups(IExistableAgencyBanker model)
        {
            if (model == null) return;
            //model.AgencyBanker.AccountTypes = lookupService.GetBankAccountTypes();
            //model.AgencyBanker.Banks = lookupService.GetBanks();
            // model.AgencyBanker.Address.States = lookupService.GetStates();
        }

        //Upload Registration
        public List<RegistrationResponseViewModel> GetRegistrations(Stream stream, bool isOld)
        {
            CsvFileReader reader = new CsvFileReader(stream, EmptyLineBehavior.NoColumns);

            var parameters = new List<string>();
            var output = new List<RegistrationResponseViewModel>();
            var isFirstRow = true;
            while (reader.ReadRow(parameters))
            {
                if (isFirstRow)
                {
                    isFirstRow = false;
                    parameters = new List<string>();
                    continue;
                }

                RegistrationViewModel registration;
                RegistrationResponseViewModel response;
                IRegsitrationCreator creator = GetCreator(parameters);
                if (creator != null)
                {

                    response = new RegistrationResponseViewModel();
                    registration = creator.Create(parameters);
                    //   registrationService.Check(registration);
                    response.Model = registration;
                    response.Errors = creator.ErrorHandler;
                    Validate(response);
                    output.Add(response);
                }

                parameters = new List<string>();
            }
            return output;
        }
        public List<RegistrationResponseViewModel> GetRegistrations(Stream stream)
        {
            return GetRegistrations(stream, false);
        }
        private void Validate(RegistrationResponseViewModel model)
        {

            /*if (!registrationService.Check(model.Model)) {
                 foreach (var error in registrationService.CurrentContext.ValidationMessages) {
                     model.Errors.Add(error.ErrorKey, error.ErrorMessage);
                 }
             }*/


        }
        public IRegsitrationCreator GetCreator(List<string> parameters)
        {
            IRegsitrationCreator creator = null;
            if (parameters.Count > 0)
            {

                switch (parameters[2])
                {
                    case LookupConstants.AgencyType.Individual:
                        creator = ObjectContainer.Container.Resolve<IndividualRegistrationCreator>();
                        break;
                    case LookupConstants.AgencyType.Partnership:
                        creator = ObjectContainer.Container.Resolve<PartnerShipRegistrationCreator>();
                        break;
                    case LookupConstants.AgencyType.PrivateLimitedCompany:
                    case LookupConstants.AgencyType.PublicLimitedCompany:
                    case LookupConstants.AgencyType.Cooperative:
                    case LookupConstants.AgencyType.GovernmentAgency:
                        creator = ObjectContainer.Container.Resolve<CorporateRegistrationCreator>();
                        break;
                    case LookupConstants.AgencyType.SoleProprietorship:
                        creator = ObjectContainer.Container.Resolve<SoleProprietorshipRegistrationCreator>();
                        break;
                    default:
                        break;
                }
            }

            return creator;
        }

        long GetFamilyIntermediaryTypeId()
        {
            return lookupRepository.GetAll<LookupIntermediaryType>().Where(p => p.Code == LookupConstants.IntermediaryType.Family).First().ID;
        }


    }// class
}// namesp