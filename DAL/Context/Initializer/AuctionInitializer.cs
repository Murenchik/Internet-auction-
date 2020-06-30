using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context.Initializer
{
    public class AuctionInitializer : DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            context.Users.AddRange(
                new List<Model.User>
                {
                    new Model.User { Login = "AdminDenis", Password = "admin647", Privilege = DAL.Model.User.AdminPrivilege },
                    new Model.User { Login = "AdminVadym", Password = "admin111", Privilege = DAL.Model.User.AdminPrivilege }
                }
            );
            context.Categories.Add(new Model.Category { CategoryName = "just_test" });
            context.SaveChanges();
        }
    }
}
