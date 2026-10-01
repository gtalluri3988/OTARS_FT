using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.ViewModels.Masters {
    public class GuarantorTypeViewModel
    {

         public string Code { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public byte[] RecordVersion { get; set; }
    }
}
