using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Data.Enums;

namespace Data.EntityClasses
{
    public class User
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; private set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string Email { get; set; }
        public  Roles  Role { get; set; }
        public string UserPassword { get; set; }

        
        public string? Token { get; private set; }


        
        public ICollection<UserTicket>? UserTickets { get; private set; }


        
    }
}
