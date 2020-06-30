using System.Collections.Generic;

namespace OnlineAuction.Model
{
    public class Subcategory
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public Category Category { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
