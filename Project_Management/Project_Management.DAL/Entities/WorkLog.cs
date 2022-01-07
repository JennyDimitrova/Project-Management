

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.DAL.Entities
{
    public class WorkLog : BaseEntity
    {
        public virtual int? TaskID { get; set; }
        public virtual int? UserId { get; set; }
        public int WorkingHours { get; set; }

        public WorkLog()
        {
            Created = DateTime.Now;
            EditedAt = DateTime.Now;
        }
    }
}
