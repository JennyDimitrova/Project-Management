using System;
using System.Collections.Generic;

namespace Project_Management.DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Project()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
            Tasks = new List<Task>();
            Teams = new List<Team>();
            Users = new List<User>();
        }
    }
}
