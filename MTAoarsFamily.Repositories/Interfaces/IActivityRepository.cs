using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Repositories.Interfaces {

    public interface IActivityRepository : IRepository<Activity> {

        Activity Get(string code);

    }// interface

}// class
