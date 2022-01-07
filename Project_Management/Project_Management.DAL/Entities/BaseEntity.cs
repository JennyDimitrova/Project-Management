using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.DAL.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public int CreatorID { get; set; }
        public DateTime EditedAt { get; set; }
        public int EditorID { get; set; }
    }
}
