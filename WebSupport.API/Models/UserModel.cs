using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSupport.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public string CreateTime { get; set; }
        public string Group { get; set; }
    }
}
