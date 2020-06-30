using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Model
{
    public class User
    {
        public static readonly int AdminPrivilege = 32;
        public static readonly int ManagerPrivilege = 16;
        public static readonly int UserPrivilege = 8;

        [Key]
        [MaxLength(20)]
        public string Login { get; set; }
        [MinLength(3)]
        public string Password { get; set; }
        public int Privilege { get; set; }
        public ICollection<Lot> Lots { get; set; }
    }
}
