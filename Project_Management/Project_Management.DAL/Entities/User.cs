using System;
using System.Collections.Generic;


namespace Project_Management.DAL.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<WorkLog> WorkLogs { get; set; }

        public User()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
        }
    }
}
