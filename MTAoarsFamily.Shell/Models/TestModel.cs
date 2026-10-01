using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MTAoarsFamily.Shell.Models {
    public class TestModel {

        [Required(ErrorMessage="Information required to continue")]
        public string Name { get; set; }

    }
}