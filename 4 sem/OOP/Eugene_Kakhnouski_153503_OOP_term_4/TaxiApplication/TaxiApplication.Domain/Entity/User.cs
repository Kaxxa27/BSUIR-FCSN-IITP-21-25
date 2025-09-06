namespace TaxiApplication.Domain.Entity;

public class User : Entity
{
	public Role Role { get; set; }

    [Required(ErrorMessage = "Укажите логин.")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Укажите пароль.")]
    public string Password { get; set; } = null!;

	[NotMapped]
	[Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    [Required(ErrorMessage = "Повторите пароль.")]
    public string PasswordConfirm { get; set; } = null!;
}
