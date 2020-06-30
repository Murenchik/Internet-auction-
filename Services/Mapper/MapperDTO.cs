using AutoMapper;

namespace Services.Mapper
{
    public class MapperDTO
    {
        public static readonly MapperDTO Instance = new MapperDTO();
        public static IMapper Mapper => mapper;

        private static IMapper mapper;
        private MapperDTO()
        {
            Initialize();
        }

        private void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.User, DAL.Model.User>();
                cfg.CreateMap<Model.Lot, DAL.Model.Lot>();
                cfg.CreateMap<Model.Category, DAL.Model.Category>();
                cfg.CreateMap<Model.Subcategory, DAL.Model.Subcategory>();

                cfg.CreateMap<DAL.Model.User, Model.User>();
                cfg.CreateMap<DAL.Model.Lot, Model.Lot>();
                cfg.CreateMap<DAL.Model.Category, Model.Category>();
                cfg.CreateMap<DAL.Model.Subcategory, Model.Subcategory>();
            });
            mapper = config.CreateMapper();
        }
    }
}
