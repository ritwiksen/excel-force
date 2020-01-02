namespace ExcelForce.Foundation.CoreServices.Authentication
{
    public interface IAuthenticationManager<T, U> where T : IAuthenticationRequest
        where U : IAuthenticationResponse
    {
        U Login(T request);
        U Logout(string token,string instanceurl);
    }
}
