using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MTAoarsFamily.ViewModels.Operations {
    public class GuarantorViewModel {

        [Display(Name="Guarantor Type")]
        public LookupItem GuarantorType { get; set; }

        [Display(Name="Details")]
        public string Details { get; set; }

        [Display(Name = "Amount")]
        public decimal? Amount { get; set; }

        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }
       
    }
}
