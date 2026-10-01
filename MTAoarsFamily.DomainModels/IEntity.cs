using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTAoarsFamily.DomainModels {
    public interface IEntity {
        long ID { get; set; }
        byte[] RecordVersion { get; set; }
        long? CreatedBy { get; set; }
        DateTime? CreatedDate { get; set; }
        long? ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
