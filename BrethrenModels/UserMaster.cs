using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrethrenModels
{
    public class UserMaster
    {
        public int UserId { get; set; }
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string UserPassword { get; set; }
        public string UserDisplayName { get; set; }
        public string UserRoles { get; set; }
        public string UserEmailId { get; set; }
    }
}
