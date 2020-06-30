using AutoMapper;
using DAL.Repositories;
using DAL.Repositories.RepositoriesInterfaces.InterfaceGeneric;
using DAL.RepositoryInterfaces;
using DAL.UnitOfWork;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Services
{
    public class UserService : IUserService
    {
        private IRepository<DAL.Model.User> genericRepo;
        private IUserRepository repo;
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        static object Lock = new object();

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = unitOfWork.GetRepository<IUserRepository>();
            this.genericRepo = unitOfWork.GetGenericRepository<IUserRepository, DAL.Model.User>();
            this.mapper = MapperDTO.Mapper;
        }
        public Model.User Authorize(string login, string pass)
        {
            var userModel = repo.GetByLoginAndPass(login, pass);
            return mapper.Map<DAL.Model.User, Model.User>(userModel);
        }

        public Model.User Register(string login, string pass)
        {
            lock (Lock)
            {
                if (Authenticate(login))
                {
                    throw new InvalidOperationException("User with login: " + login + " - Already exist.");
                }

                var user = new Model.User { Login = login, Password = pass, Privilege = DAL.Model.User.UserPrivilege };
                genericRepo.Insert(mapper.Map<Model.User, DAL.Model.User>(user));
                unitOfWork.Save();
                var newUser = this.repo.GetByLoginAndPass(login, pass);
                return mapper.Map<DAL.Model.User, Model.User>(newUser);
            }
        }

        protected virtual bool Authenticate(string login)
        {
            return repo.IsUserExist(login);
        }
    }
}
