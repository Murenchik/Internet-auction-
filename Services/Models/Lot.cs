using System;

namespace Services.Model
{
    public class Lot
    {
        public int LotId { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public DateTime ExpiryDate { get; set; }
        public User Admin { get; set; }
        public User User { get; set; }
        public Subcategory Subcategory { get; set; }
    }
}
