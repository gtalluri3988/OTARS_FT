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
using MTAoarsFamily.DomainModels;

namespace MTAoarsFamily.Services.Accounts {

    public class UserService : IUserService {
        IUserRepository userRepository;
        IRepository<UserLogin> userLoginRepository;

        public UserService(IUserRepository userRepository, IRepository<UserLogin> userLoginRepository) {
            this.userRepository = userRepository;
            this.userLoginRepository = userLoginRepository;
        }

        public bool Authenticate(LogOnViewModel model) {
            var user = userRepository.Get(model.UserName);
            if (user == null) return false;
            return user.Password == model.Password;
        }

        public Identity Get(long userId) {
            var user = userRepository.Get(userId);
            return Mapper.Map<Identity>(user);
        }

        public Identity Get(string userName) {
            var user = userRepository.Get(userName);
            return Mapper.Map<Identity>(user);
        }

        public long SaveLoginDetail(long userId, string hostIP) {
            var userLogin = new UserLogin { UserID = userId, HostIP = hostIP, LoginTime = DateTime.Now };
            userLoginRepository.Save(userLogin);
            userLoginRepository.SaveChanges();
            return userLogin.ID;
        }

        public void SaveLogoutDetail(long userLoginId) {
            var userLogin = userLoginRepository.Get(userLoginId);
            if (userLogin != null) {
                userLogin.LogoutTime = DateTime.Now;
                userLoginRepository.SaveChanges();
            }
        }

    }// class

}// namespace
