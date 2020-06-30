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
    public class CategoryService : ICategoryService
    {
        private IRepository<DAL.Model.Category> genericRepo;
        private ICategoryRepository repo;
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        public CategoryService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = unitOfWork.GetRepository<ICategoryRepository>();
            this.genericRepo = unitOfWork.GetGenericRepository<ICategoryRepository, DAL.Model.Category>();
            this.mapper = MapperDTO.Mapper;
        }

        public ICollection<Model.Category> GetCategories()
        {
            var categories = genericRepo.Get().ToList();
            var categoriesDTO = mapper.Map<ICollection<DAL.Model.Category>, ICollection<Model.Category>>(categories);
            return categoriesDTO;
        }
    }
}
