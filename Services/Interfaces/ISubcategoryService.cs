using System.Collections.Generic;

namespace Services
{
    public interface ISubcategoryService
    {
        ICollection<Model.Subcategory> GetSubcategoriesForCategory(Model.Category category);
    }
}
