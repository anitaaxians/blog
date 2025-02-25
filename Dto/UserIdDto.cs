namespace BlogManagementAPI.Dto
{
    public class UserIdDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<BlogDto> UserBlogs { get; set; } = new();
    }
}
