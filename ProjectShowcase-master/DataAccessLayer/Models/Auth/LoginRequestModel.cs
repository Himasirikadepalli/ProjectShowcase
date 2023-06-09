using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Auth
{
    public class LoginRequestModel
    {
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}