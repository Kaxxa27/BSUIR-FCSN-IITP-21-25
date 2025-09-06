namespace TaxiApplication.BLL.Interfaces;

public interface IAccountService
{
    public Task<BaseResponse<ClaimsPrincipal>> LoginAsync(LoginViewModel loginViewModel);
    public Task<BaseResponse<ClaimsPrincipal>> RegistrationAsync(Client client);
}
