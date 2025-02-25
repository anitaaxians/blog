using BlogManagementAPI.Data.Model;

namespace BlogManagementAPI.Services
{
    public interface IAuthService
    {
        Task<string> GenerateTokenString(LoginDto user);
        //Task<bool> Login(LoginUser user);
        Task<bool> Login(LoginDto user);
        Task<bool> RegisterUser(LoginUser user);
    }
}