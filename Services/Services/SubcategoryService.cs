using AutoMapper;
using DAL.Repositories;
using DAL.Repositories.RepositoriesInterfaces.InterfaceGeneric;
using DAL.RepositoryInterfaces;
using DAL.UnitOfWork;
using Services.Mapper;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private IRepository<DAL.Model.Subcategory> genericRepo;
        private ISubcategoryRepository repo;
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        public SubcategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = unitOfWork.GetRepository<ISubcategoryRepository>();
            this.genericRepo = unitOfWork.GetGenericRepository<ISubcategoryRepository, DAL.Model.Subcategory>();
            this.mapper = MapperDTO.Mapper;
        }

        public ICollection<Model.Subcategory> GetSubcategoriesForCategory(Model.Category category)
        {
            var subcategoriesModel = genericRepo.Get(s => s.Category.CategoryName.Equals(category.CategoryName)).ToList();
            var subcategories = mapper.Map<ICollection<DAL.Model.Subcategory>, ICollection<Model.Subcategory>>(subcategoriesModel);
            return subcategories;
        }
    }
}
