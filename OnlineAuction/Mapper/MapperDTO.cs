using AutoMapper;

namespace OnlineAuction.Mapper
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
                cfg.CreateMap<Model.User, Services.Model.User>();
                cfg.CreateMap<Model.Lot, Services.Model.Lot>();
                cfg.CreateMap<Model.Category, Services.Model.Category>();
                cfg.CreateMap<Model.Subcategory, Services.Model.Subcategory>();

                cfg.CreateMap<Services.Model.Lot, Model.Lot>();
                cfg.CreateMap<Services.Model.User, Model.User>();
                cfg.CreateMap<Services.Model.Category, Model.Category>();
                cfg.CreateMap<Services.Model.Subcategory, Model.Subcategory>();
            });
            mapper = config.CreateMapper();
        }
    }
}
