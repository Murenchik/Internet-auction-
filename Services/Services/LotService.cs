using AutoMapper;
using DAL.Repositories;
using DAL.Repositories.RepositoriesInterfaces.InterfaceGeneric;
using DAL.RepositoryInterfaces;
using DAL.UnitOfWork;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class LotService : ILotService
    {
        private IRepository<DAL.Model.Lot> genericRepo;
        private ILotRepository repo;
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        public LotService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = unitOfWork.GetRepository<ILotRepository>();
            this.genericRepo = unitOfWork.GetGenericRepository<ILotRepository, DAL.Model.Lot>();
            this.mapper = MapperDTO.Mapper;
        }

        public ICollection<Model.Lot> All()
        {
            
            var lots = genericRepo.Get(null,null,"Admin, User, Category, Subcategory").ToList();
            var dto = mapper.Map<ICollection<DAL.Model.Lot>, ICollection<Model.Lot>>(lots);
            return dto;
        }

        public void Add(string description, DateTime expiryDate, Model.User userModel, Model.Category categoryModel, Model.Subcategory subcategoryModel)
        {
            if (expiryDate.CompareTo(DateTime.Today) < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid expiry date: " + expiryDate + " Today: " + DateTime.Today);
            }

            NullUserModelOrCategory(userModel, categoryModel);

            var userRepo = unitOfWork.GetRepository<IUserRepository>();
            var userGenericRepo = unitOfWork.GetGenericRepository<IUserRepository, DAL.Model.User>();
            var categoryGenericRepo = unitOfWork.GetGenericRepository<ICategoryRepository, DAL.Model.Category>();
            var subcategoryGenericRepo = unitOfWork.GetGenericRepository<ISubcategoryRepository, DAL.Model.Subcategory>();

            var user = userGenericRepo.Get(
                u => u.Login == userModel.Login && u.Password == userModel.Password, null, "Lots")
                .FirstOrDefault();

            NullUserOrLowPrivilege(userModel, user);

            var category = categoryGenericRepo.Get(c => c.CategoryId == categoryModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                throw new InvalidOperationException("No category with such id: " + categoryModel.CategoryId);
            }
            var subcategory = subcategoryModel != null ? subcategoryGenericRepo.Get(subcategoryModel.SubcategoryId) : null;

            var userIsAdmin = 
                user.Privilege.Equals(DAL.Model.User.AdminPrivilege) || 
                user.Privilege.Equals(DAL.Model.User.ManagerPrivilege) ? user : null;

            DAL.Model.Lot lot = CreateLot(
                description,
                user,
                userIsAdmin,
                category, 
                subcategory,
                expiryDate
                );

            user.Lots.Add(lot);
            unitOfWork.Update(user);
            unitOfWork.Save();
        }

        public void Delete(Model.Lot lotModel, Model.User user)
        {
            if (lotModel == null)
            {
                throw new InvalidOperationException("No lot");
            }

            var lot = genericRepo.Get(l => AuthenticateUser(lotModel, user, l));

            if (lot == null)
            {
                throw new InvalidOperationException("No such lot");
            }

            genericRepo.Delete(lotModel.LotId);

            unitOfWork.Save();
        }

        private DAL.Model.Lot CreateLot(string description,
            DAL.Model.User user,
            DAL.Model.User admin,
            DAL.Model.Category category,
            DAL.Model.Subcategory subcategory,
            DateTime expiryDate)
        {
            return new DAL.Model.Lot
            {
                Description = description,
                User = user,
                Admin = admin ?? null,
                Category = category,
                Subcategory = subcategory,
                ExpiryDate = expiryDate,
            };
        }

        private void NullUserOrLowPrivilege(Model.User userModel, DAL.Model.User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("No such user: " + userModel.Login + ":" + userModel.Password);
            }
            if (user.Privilege <= DAL.Model.User.UserPrivilege)
            {
                throw new InvalidOperationException("Low permission: " + user.Login);
            }
        }

        private void NullUserModelOrCategory(Model.User userModel, Model.Category categoryModel)
        {
            if (userModel == null)
            {
                throw new InvalidOperationException("No user");
            }
            if (categoryModel == null)
            {
                throw new InvalidOperationException("No category");
            }
        }

        private bool AuthenticateUser(Model.Lot lotModel, Model.User user, DAL.Model.Lot lot)
        {
            return lot.User.Login == user.Login &&
                lot.User.Password == user.Password &&
                lot.LotId == lotModel.LotId;
        }
    }
}
