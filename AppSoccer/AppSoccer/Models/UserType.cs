using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSoccer.Models
{
    public class UserType
    {
        [PrimaryKey]
        public int UserTypeId { get; set; }
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<User> Users { get; set; }

        public override int GetHashCode()
        {
            return UserTypeId;
        }
    }
}
