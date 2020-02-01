using alexander_neumann.api.Auth;
using alexander_neumann.api.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace alexander_neumann.api.Services
{
    public interface IUserService
    {
        Task<ClaimsPrincipal> GetCurrentClaimsPrincipal();
        Task<User> GetCurrentUser();
    }

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public UserService(IHttpContextAccessor httpContextAccessor, AppDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ClaimsPrincipal> GetCurrentClaimsPrincipal()
        {
            if (_httpContextAccessor.HttpContext.User == null)
                return null;

            return await Task.FromResult<ClaimsPrincipal>(_httpContextAccessor.HttpContext.User);
        }

        public async Task<User> GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User == null)
                return null;
            
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return null;

            var username = _httpContextAccessor.HttpContext.User.FindFirstValue("name");
            var givenname = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            var surname = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);
            var country = _httpContextAccessor.HttpContext.User.FindFirstValue("country");
            var email = _httpContextAccessor.HttpContext.User.FindFirstValue("emails");

            var user = new User
            {
                Id = userId,
                Email = email,
                GivenName = givenname,
                Surname = surname,
                Username = username,
                Country = country
            };

            return await Task.FromResult<User>(user);
        }
    }
}
