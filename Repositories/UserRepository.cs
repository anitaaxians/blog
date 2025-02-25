using BlogManagementAPI.Data;
using BlogManagementAPI.Data.Model;
using BlogManagementAPI.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogManagementAPI.Repositories
{
    public class UserRepository
    {
        private readonly BlogDbContext _blogDbContext;
        public UserRepository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;

        }
        public List<ApplicationUser> GetUsers()
        {
            return _blogDbContext.Users.ToList();
        }
        public async Task<string> AddUser(UserCreateDto user)
        {
            ApplicationUser newUser = new ApplicationUser();
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.Birthdate = user.Birthdate;
            newUser.Email = user.Email;
            newUser.IsActive = true;

            _blogDbContext.Users.Add(newUser);
            await _blogDbContext.SaveChangesAsync();
            return newUser.Id;
        }
        public async Task<UserIdDto?> GetUserById(string id)
        {
            var result = await _blogDbContext.Users.Include(x => x.Blogs).FirstOrDefaultAsync(x => x.Id == id);



            UserIdDto userIdDto = new()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Birthdate = result.Birthdate,
                Email = result.Email,
                IsActive = result.IsActive
            };

            foreach (var blog in result.Blogs)
            {
                BlogDto blogDto = new BlogDto();
                blogDto.BlogId = blog.BlogId;
                blogDto.Title = blog.Title;
                blogDto.CreatedOn = blog.CreatedOn;
                blogDto.ModifiedOn = blog.ModifiedOn;
                blogDto.IsActive = blog.IsActive;
                blogDto.Content = blog.Content;
                userIdDto.UserBlogs.Add(blogDto);
            }
            return userIdDto;
        }
        public async Task<bool> UpdateUser(UserIdDto dto)
        {
            var user = await _blogDbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

            if (user != null)
            {
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.Birthdate = dto.Birthdate;
                user.IsActive = dto.IsActive;
                _blogDbContext.Users.Update(user);
                await _blogDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteUser(string id)
        {
            var user = await _blogDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                user.IsActive = false;
                _blogDbContext.Users.Update(user);
                await _blogDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<BlogDto>> GetUserPosts(string id)
        {
            List<BlogDto> result = new();
            var blogs = await _blogDbContext.Blog.Where(x => x.UserId == id).ToListAsync();

            foreach (var blog in blogs)
            {
                BlogDto blogDto = new();

                blogDto.BlogId = blog.BlogId;
                blogDto.Title = blog.Title;
                blogDto.CreatedOn = blog.CreatedOn;
                blogDto.ModifiedOn = blog.ModifiedOn;
                blogDto.IsActive = blog.IsActive;
                blogDto.Content = blog.Content;

                result.Add(blogDto);
            }
            return result;
        }
    }
}

