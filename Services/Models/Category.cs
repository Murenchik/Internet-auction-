using System.Collections.Generic;

namespace Services.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Lot> Lots { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
