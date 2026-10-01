using MTAoarsFamily.DomainModels;
using AutoMapper;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Interfaces;


namespace MTAoarsFamily.Mappers.Operations {

    class ConditionalPassportNumberResolver : ValueResolver<IMemberViewModel, string> {
        ILookupRepository lookupRepository;

        public ConditionalPassportNumberResolver(ILookupRepository lookupRepository) {
            this.lookupRepository = lookupRepository;
            
        }

        protected override string ResolveCore(IMemberViewModel source) {
            if (source.ICType.Code == LookupConstants.ICTypes.PoliceArmyPassport)
                return source.ICNumber;
            return null;
        }

    }

}
