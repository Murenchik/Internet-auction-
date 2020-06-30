using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Subcategory
    {
        public int SubcategoryId { get; set; }
        [Required]
        public string SubcategoryName { get; set; }
        [Required]
        public Category Category { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
