using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public ICollection<Lot> Lots { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
