using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DTO_Models.Requests.Teams
{
    public class TeamRequest
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
    }
}
