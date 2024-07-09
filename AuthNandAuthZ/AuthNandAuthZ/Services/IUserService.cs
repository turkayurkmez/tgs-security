using AuthNandAuthZ.DataModels;
using AuthNandAuthZ.Models;

namespace AuthNandAuthZ.Services
{
    public interface IUserService
    {
        DataModels.RealUser? ValidateUser(string userName, string password);

        void RegisterUser(RealUser user);
    }
}