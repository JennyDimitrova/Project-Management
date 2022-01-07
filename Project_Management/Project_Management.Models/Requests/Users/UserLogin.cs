using Project_Management.DAL.Entities;
using System.ComponentModel.DataAnnotations;


namespace Project_Management.DTO_Models.Requests.Users
{
    public class UserLogin
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}
