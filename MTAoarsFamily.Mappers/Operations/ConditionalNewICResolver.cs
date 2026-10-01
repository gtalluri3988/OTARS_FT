using MTAoarsFamily.DomainModels;
using AutoMapper;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Utilities.Constants;
using MTAoarsFamily.ViewModels.Interfaces;


namespace MTAoarsFamily.Mappers.Operations {

    class ConditionalNewICResolver : ValueResolver<IMemberViewModel, string> {
        ILookupRepository lookupRepository;

        public ConditionalNewICResolver(ILookupRepository lookupRepository) {
            this.lookupRepository = lookupRepository;
            
        }

        protected override string ResolveCore(IMemberViewModel source) {
            if (source.ICType.Code == LookupConstants.ICTypes.NewIc)
                return source.ICNumber.Trim();
            return null;
        }

    }

}
