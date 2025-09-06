namespace TaxiApplication.Domain.Enum;

public enum Tariff
{
	[Display(Name = "Эконом")]
	Economy,
	[Display(Name = "Комфорт")]
	Comfort,
	[Display(Name = "Бизнес")]
	Business,
	[Display(Name = "VIP")]
	VIP
}
