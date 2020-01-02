using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Models;

namespace ExcelForce.Business.Interfaces
{
    public interface IUserAuthenticationService
    {
        ServiceResponseModel<bool> Login(string userName, string password, string securityToken, string connectionProfile);
        ServiceResponseModel<bool> Logout();
    }
}
