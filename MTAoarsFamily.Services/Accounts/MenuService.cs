using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.ViewModels.Accounts;
using MTAoarsFamily.Repositories.Interfaces;
using MTAoarsFamily.ViewModels.Shared;
using AutoMapper;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Services.Accounts {

    public class MenuService : IMenuService {
        IMenuRepository menuRepository;

        public MenuService(IMenuRepository menuRepository) {
            this.menuRepository = menuRepository;
        }

        public List<MenuItem> GetMenus() {
            return menuRepository.GetMenus();
        }

        public List<MenuItem> GetMenus(long roleId) {
            return menuRepository.GetMenus(roleId);
        }

        public List<long> GetRoleMenus(long roleId) {
            return menuRepository.GetRoleMenus(roleId);
        }

        public void Save(long roleId, IEnumerable<long> menus) {
            menuRepository.Save(roleId, menus);
        }

    }// class

}// namespace
