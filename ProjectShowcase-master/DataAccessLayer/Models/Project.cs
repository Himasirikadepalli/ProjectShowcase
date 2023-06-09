using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Project
    {
        [Key]
        public string ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required] 
        public string Keywords { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        [RegularExpression("Pending|Approved|Rejected")]
        public string Status { get; set; } = "Pending";
    }
}
