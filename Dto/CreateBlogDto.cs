namespace BlogManagementAPI.Dto
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string UserId { get; set; }
    }
}
