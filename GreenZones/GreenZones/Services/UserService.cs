using GreenZones.DAL;
using GreenZones.Models;
using Microsoft.Identity.Web;

namespace GreenZones.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitofWork _unitofWork;
        private IHttpContextAccessor? _httpContextAccessor;
        public UserService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public Task<User> AddUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var userId = _httpContextAccessor.HttpContext?.User.GetObjectId();
            var userDisplayName = _httpContextAccessor.HttpContext?.User.GetDisplayName();

            var user = new User
            {
                UserPrincipalName = userId,
                DisplayName = userDisplayName,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                UpdatedBy = "System",
                UpdatedDate = DateTime.Now
            };
            _unitofWork.UserRepository.AddAsync(user);
            _unitofWork.Save();

            return Task.FromResult(user);
        }

        public async Task<User> GetUserByIdAsync(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var users = await _unitofWork.UserRepository.FindAsync(u => u.UserPrincipalName == _httpContextAccessor.HttpContext.User.GetObjectId());
            return users.FirstOrDefault();
        }
    }
}
