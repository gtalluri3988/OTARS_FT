using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.Repositories.Shared;
using MTAoarsFamily.DomainModels;
using MTAoarsFamily.Utilities.Config;
using MTAoarsFamily.Utilities.Constants;
using System.Data;

namespace MTAoarsFamily.Repositories.Operations {

    public class ALCRepository : GenericRepository<ALCHeader>, IALCRepository {
        ConfigManager config;
     
        public ALCRepository(EntityContext context, ConfigManager config)
            : base(context) {
                this.config = config;
        }

        public void SaveHeader(ALCHeader header) {
            if (header.ID > 0) {
                foreach (var member in header.ALCMembers) {
                    member.HeaderID = header.ID;
                }
                Attach(header);
                Context.ObjectStateManager.ChangeObjectState(header, EntityState.Modified);
                Context.Addresses.Attach(header.Address);
                Context.ObjectStateManager.ChangeObjectState(header.Address, EntityState.Modified);
                foreach (var member in header.ALCMembers) {
                    if (member.ID > 0) {
                        //Context.ALCMembers.Attach(member);
                        Context.ObjectStateManager.ChangeObjectState(member, EntityState.Modified);
                    } else {
                        //Context.ALCMembers.AddObject(member);
                        Context.ObjectStateManager.ChangeObjectState(member, EntityState.Added);
                    }
                }
                
                
            } else {
                Save(header);
            }
            SaveChanges();
        }

    }// class

}// namespace
