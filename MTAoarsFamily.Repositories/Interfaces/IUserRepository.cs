using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {
    public interface IUserRepository : IRepository<User> {

        User Get(string userName);
        User Get(long userID);
    }
}
