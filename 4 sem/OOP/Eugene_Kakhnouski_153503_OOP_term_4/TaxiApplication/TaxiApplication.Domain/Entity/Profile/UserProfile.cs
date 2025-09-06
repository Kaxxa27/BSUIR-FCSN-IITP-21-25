namespace TaxiApplication.Domain.Entity.Profile;

public class UserProfile : Entity
{

	[Required(ErrorMessage = "Укажите имя пользователя.")]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 50 символов.")]
	public  string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Укажите фамилию пользователя.")]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 50 символов.")]
	public  string Surname { get; set; } = string.Empty;

	
	[Range(1,100,ErrorMessage = "Неккоректный возраст (1-100).")]
    [Required(ErrorMessage = "Неккоректный возраст (1-100).")]
    public int Age { get; set; }

    [RegularExpression(@"([A-Za-z0-9]+[.-_])*[A-Za-z0-9]+@[A-Za-z0-9-]+(\.[A-Z|a-z]{2,})+", ErrorMessage = "Некорректный адрес электронной почты.")]
	[Required(ErrorMessage = "Укажите вашу электронную почту.")]
	public string Email { get; set; } = string.Empty;

	[Phone(ErrorMessage = "Некорректный номер телефона.")]
	[Required(ErrorMessage = "Укажите ваш номер телефона.")]
	public string PhoneNumber { get; set; } = string.Empty;
}
