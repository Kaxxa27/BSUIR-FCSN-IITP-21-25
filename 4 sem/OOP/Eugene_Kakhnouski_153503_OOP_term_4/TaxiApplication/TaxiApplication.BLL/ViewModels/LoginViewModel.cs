namespace TaxiApplication.BLL.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Укажите логин.")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Укажите пароль.")]
    public string Password { get; set; } = null!;
}
