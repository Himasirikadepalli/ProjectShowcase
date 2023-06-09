using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer.Models.Auth
{
    public enum Gender
    {
        Male,
        Female,
        Others
    }



    public class MyUser : IdentityUser
    {
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }



        public string LastName { get; set; }



        [EnumDataType(typeof(Gender))]
        [Required]
        public Gender Gender { get; set; }



        [EnumDataType(typeof(Roles))]
        public string RoleAlloted { get; set; }
    }
}
