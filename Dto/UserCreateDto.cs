using System.ComponentModel.DataAnnotations;

namespace BlogManagementAPI.Dto
{
    public class UserCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
    }
}
