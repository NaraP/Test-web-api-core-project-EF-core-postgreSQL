using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ABB.RCS.SystemManagament.Entities
{
    public class User
    {
        public int UserId { get; set; }
       // public string RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public DateTime LastLoginDate { get; set; }

    }
}
