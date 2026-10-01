using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.ViewModels.Accounts;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Services.Interfaces {
    public interface IMenuService {

        List<MenuItem> GetMenus();

        List<MenuItem> GetMenus(long roleId);

        void Save(long roleId, IEnumerable<long> menus);

        List<long> GetRoleMenus(long roleId);
    }
}
