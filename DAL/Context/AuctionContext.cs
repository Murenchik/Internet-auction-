using DAL.Context.Initializer;
using DAL.Model;
using System.Data.Entity;

namespace DAL
{
    public class AuctionContext : DbContext
    {
        public AuctionContext() : base("AuctionDb")
        {
            Database.SetInitializer(new AuctionInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Lot> Lots { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
