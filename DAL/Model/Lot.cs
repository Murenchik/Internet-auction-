using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class Lot
    {
        [Key]
        public int LotId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public Category Category { get; set; }
        public User Admin { get; set; }
        public User User { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
