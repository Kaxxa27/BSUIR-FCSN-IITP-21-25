namespace TaxiApplication.Domain.Enum;

public enum Role
{
	[Display(Name = "Пользователь")]
	Client = 0,
	//[Display(Name = "Водитель")]
	//Driver = 1,
	[Display(Name = "Админ")]
	Admin = 2,
}
