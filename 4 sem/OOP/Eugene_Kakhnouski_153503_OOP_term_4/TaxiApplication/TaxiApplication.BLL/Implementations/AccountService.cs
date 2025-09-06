namespace TaxiApplication.BLL.Implementations;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<BaseResponse<ClaimsPrincipal>> LoginAsync(LoginViewModel loginViewModel)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(u => u.Login == loginViewModel.Login);

            if (user is null)
            {
                return new BaseResponse<ClaimsPrincipal>()
                {
                    Description = "Пользователь не найден.",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            if (user.Password != loginViewModel.Password)
            {
                return new BaseResponse<ClaimsPrincipal>()
                {
                    Description = "Неверный логин или пароль.",
                    StatusCode = StatusCode.WrongPassword
                };
            }

            return new BaseResponse<ClaimsPrincipal>()
            {
                Data = GetClaimsPrincipal(user),
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[AccountService.LoginAsync] error: {ex.Message})");
            return new BaseResponse<ClaimsPrincipal>()
            {
                StatusCode = StatusCode.AllError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    public async Task<BaseResponse<ClaimsPrincipal>> RegistrationAsync(Client client)
    {
        try
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(
                u => u.Login.Trim().ToLowerInvariant() == client.Login.Trim().ToLowerInvariant());

            if (user != null)
            {
                return new()
                {
                    Description = "Данный логин уже занят.",
                    StatusCode = StatusCode.LoginAlredyExist
                };
            }

            await _unitOfWork.ClientRepository.AddAsync(client);
           // await _unitOfWork.ClientProfileRepository.AddAsync(client.Profile);
            

            return new BaseResponse<ClaimsPrincipal>()
            {
                Data = GetClaimsPrincipal(client),
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[AccountService.RegistrationAsync] error: {ex.Message})");
            return new BaseResponse<ClaimsPrincipal>()
            {
                StatusCode = StatusCode.AllError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }

    private ClaimsPrincipal GetClaimsPrincipal(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));
    }
}
