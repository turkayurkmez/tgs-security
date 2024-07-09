using AuthNandAuthZ.Data;
using AuthNandAuthZ.DataModels;
using AuthNandAuthZ.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthNandAuthZ.Services
{
    public class RealUserService : IUserService
    {
        private readonly SecureDbContext secureDbContext;

        public RealUserService(SecureDbContext secureDbContext)
        {
            this.secureDbContext = secureDbContext;
        }

        public void RegisterUser(RealUser user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            secureDbContext.RealUsers.Add(user);
            secureDbContext.SaveChanges();
        }

        public DataModels.RealUser? ValidateUser(string userName, string password)
        {
            var user = secureDbContext.RealUsers.SingleOrDefault(x => x.UserName == userName);
            var isVerified =  BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isVerified ? user : null;

        }
    }
}
