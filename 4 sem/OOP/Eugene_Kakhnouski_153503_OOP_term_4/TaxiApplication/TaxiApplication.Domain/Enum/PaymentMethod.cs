namespace TaxiApplication.Domain.Enum;

public enum PaymentMethod
{
	[Display(Name = "Наличные")]
	Cash,
	[Display(Name = "Карта")]
	CreditCard,
	PayPal,
	ApplePay,
	GooglePay
}
