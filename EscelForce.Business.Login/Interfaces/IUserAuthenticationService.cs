using ExcelForce.Foundation.Authentication.Models;

namespace ExcelForce.Business.Interfaces
{
    public interface IUserAuthenticationService
    {
        bool Login(string userName, string password, string connectionProfile);
    }
}
