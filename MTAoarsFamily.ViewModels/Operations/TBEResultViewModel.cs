using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.Attributes;
using MTAoarsFamily.Utilities.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.ViewModels.Operations
{
    public class TBEResultViewModel
    {
        [Binder(typeof(JsonModelBinder))]
        [Dependency]
        public TBEEnquirySearchViewModel TBEResult { get; set; }

    }
}
