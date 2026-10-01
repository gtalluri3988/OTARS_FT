using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Services.Shared;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Validators;
using MTAoarsFamily.Repositories.Interfaces;
using AutoMapper;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Extensions;
using MTAoarsFamily.Utilities.Constants;
using System.Transactions;
using MTAoarsFamily.ViewModels.Notifications;
using MTAoarsFamily.Mappers.Operations;
using MTAoarsFamily.ViewModels.Operations;
using System.Data;

namespace MTAoarsFamily.Services.Operations {
    public class ALCService : BaseService, IALCService {

        IALCRepository alcRepository;
        public ALCService(IValidationProvider validationProvider, IALCRepository alcRepository)
            : base(validationProvider) {
                this.alcRepository = alcRepository;
                ALCHeader h;
        }

        public void Save(ALCHeaderViewModel input) {
            var header = Mapper.Map<ALCHeaderViewModel, ALCHeader>(input);
            alcRepository.SaveHeader(header);
        }
        
    }// class
}// namespace
