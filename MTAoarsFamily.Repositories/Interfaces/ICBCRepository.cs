using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;


namespace MTAoarsFamily.Repositories.Interfaces {
    public interface ICBCRepository : IRepository<CBCHeader> {

        CBCHeader Get(int year, int quarter);

        IEnumerable<CBCPivotDetail> GetPivot(int year, int quarter);

        IEnumerable<CBCDetail> GetDetails(int year, int quarter, long agencyId);

        IEnumerable<CBCDetail> GetDetails(int quarterValue);

    }
}
