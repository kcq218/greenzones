using GreenZones.Models;

namespace GreenZones.Services
{
    public interface IUserService
    {
        public Task<User> GetUserByIdAsync(IHttpContextAccessor httpContextAccessor);
        public Task<User> AddUser(IHttpContextAccessor httpContextAccessor);
    }
}
