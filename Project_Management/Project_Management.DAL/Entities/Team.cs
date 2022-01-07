using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        public Team()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
            Projects = new List<Project>();
            Users = new List<User>();
        }
    }
}
