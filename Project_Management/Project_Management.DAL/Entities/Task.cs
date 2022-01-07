using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Project_Management.DAL.Entities
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public virtual int ProjectID { get; set; }
        public virtual Project Project { get; set; }
        public virtual int AssignedUserID { get; set; }
        public virtual User AssignedUser { get; set; }
        public virtual  ICollection<WorkLog> WorkLogs { get; set; }

        public Task()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
            WorkLogs = new List<WorkLog>();
        }
    }
}
