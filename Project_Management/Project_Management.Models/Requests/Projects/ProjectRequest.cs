using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DTO_Models.Requests.Projects
{
    public class ProjectRequest
    {
        [Required]
        [MinLength(4)]
        public string Title { get; set; }
    }
}
