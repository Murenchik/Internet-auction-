using System.Collections.Generic;

namespace Services
{
    public interface ILotService
    {
        void Add(string description, System.DateTime expiryDate, Model.User userModel, Model.Category categoryModel, Model.Subcategory subcategoryModel);
        void Delete(Model.Lot lotModel, Model.User user);
        ICollection<Model.Lot> All();
    }
}
