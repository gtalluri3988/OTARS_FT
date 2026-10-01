using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Config;

namespace MTAoarsFamily.Repositories.Operations {

    public class IbfimResultRepository : GenericRepository<IbfimResult>, IIbfimResultRepository {

        public IbfimResultRepository(EntityContext context)
            : base(context) {
              
        }
       
        public IEnumerable<IbfimResult> Search(string icNumber){
            return Context.IbfimResults.Where(p => p.ICNumber == icNumber).AsEnumerable();
        }

        public IbfimResult Search(int id)
        {
            return Context.IbfimResults.Where(p => p.ID == id).FirstOrDefault();
        }

        public void Update(IbfimResult source,out string message)
        {
            var Result = Context.IbfimResults.Where(p => p.ID == source.ID).FirstOrDefault();

            Result.Name = source.Name;
            Result.ICNumber = source.ICNumber;
            Result.ExamDate = source.ExamDate;
            Result.Grade = source.Grade;
            Result.Result = source.Result;

            try
            {
                SaveChanges();
                message = "Successfully Save";
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
           
        }

    }// class

}// namespace
