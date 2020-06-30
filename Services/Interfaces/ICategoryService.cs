using System.Collections.Generic;

namespace Services
{
    public interface ICategoryService
    {
        ICollection<Model.Category> GetCategories();
    }
}
