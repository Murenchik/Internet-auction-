using System.Collections.Generic;

namespace OnlineAuction.Model
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Privilege { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
