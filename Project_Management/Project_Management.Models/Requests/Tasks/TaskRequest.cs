using System.ComponentModel.DataAnnotations;


namespace Project_Management.DTO_Models.Requests.Tasks
{
    public class TaskRequest
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [Required]
        [MinLength(5)]
        public string Description { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public virtual int ProjectID { get; set; }
    }
}
