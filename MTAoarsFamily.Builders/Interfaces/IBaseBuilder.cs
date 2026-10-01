using MTAoarsFamily.ViewModels.Operations;
using System.Collections.Generic;
using MTAoarsFamily.Services.Shared;

namespace MTAoarsFamily.Builders.Interfaces {
    public interface IBaseBuilder {

        BuilderContext CurrentContext { get; set; }

    }// interface
}// namespace
