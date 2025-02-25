using BlogManagementAPI.Data;
using BlogManagementAPI.Data.Model;
using BlogManagementAPI.Dto;
using Microsoft.EntityFrameworkCore;

namespace BlogManagementAPI.Repositories
{
    public class BlogRepository
    {
        private readonly BlogDbContext _blogDbContext;
        public BlogRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;

        }
        public async Task<int> AddBlog(string title, string userId, string content,string shortContent)
        {

            Blog blog = new();

            blog.Title = title;
            blog.CreatedOn = DateTime.Now;
            blog.ModifiedOn = DateTime.Now;
            blog.UserId = userId;
            blog.Content = content;
            blog.ShortContent = shortContent;
            blog.IsActive = true;

            _blogDbContext.Blog.Add(blog);
            await _blogDbContext.SaveChangesAsync();

            return blog.BlogId;

        }

        public async Task<List<BlogDto>> GetAllBlogs()
        {
            List<BlogDto> blogDtos = new();
            var result = await _blogDbContext.Blog.ToListAsync();

            foreach (var blog in result)
            {
                BlogDto dto = new();
                dto.BlogId = blog.BlogId;
                dto.Title = blog.Title;
                dto.CreatedOn = blog.CreatedOn;
                dto.ModifiedOn = blog.ModifiedOn;
                dto.IsActive = blog.IsActive;
                dto.Content = blog.Content;
                dto.ShortContent = blog.ShortContent;

                blogDtos.Add(dto);
            }
            return blogDtos;
        }

        public async Task<BlogDto> GetBlogById(int blogID)
        {
            var result = await _blogDbContext.Blog.Where(x => x.BlogId == blogID).FirstOrDefaultAsync();

            BlogDto blogDto = new();

            blogDto.BlogId = result.BlogId;
            blogDto.Title = result.Title;
            blogDto.CreatedOn = result.CreatedOn;
            blogDto.ModifiedOn = result.ModifiedOn;
            blogDto.IsActive = result.IsActive;
            blogDto.Content = result.Content;
            blogDto.ShortContent = result.ShortContent;

            return blogDto;
        }

        public async Task<bool> UpdateBlog(BlogDto blogDto)
        {
            var blog = await _blogDbContext.Blog.FirstOrDefaultAsync(x => x.BlogId == blogDto.BlogId);

            if (blog != null)
            {
                blog.Title = blogDto.Title;
                blog.ModifiedOn = DateTime.Now;
                blog.IsActive = blogDto.IsActive;
                blog.Content = blogDto.Content;
                blog.ShortContent = blogDto.ShortContent;
                _blogDbContext.Blog.Update(blog);
                await _blogDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteBlog(int id)
        {
            var blog = await _blogDbContext.Blog.FirstOrDefaultAsync(x => x.BlogId == id);

            if (blog != null)
            {
                blog.IsActive = false;
                _blogDbContext.Blog.Update(blog);
                await _blogDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
