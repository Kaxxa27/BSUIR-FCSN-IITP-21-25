namespace TaxiApplication.Domain.Entity;

public class TaxiOrder : Entity
{
	public Route.Route? CurrentRoute { get; set; }
	public Tariff Tariff { get; set; }
    public double Price { get; set; }
    [Required(ErrorMessage = "Введите кол-во пассажиров.")]
	[Range(1, 5, ErrorMessage = "Кол-во пассажиров должно быть 1-4.")]
	public int NumberOfPassengers { get; set; }
	public DriverGender DriverGenderPreference { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
    public int ClientId { get; set; }

    #region IsProperties
    public bool IsWheelchairAccessible { get; set; }
	public bool IsPetFriendly { get; set; }
	public bool IsRoundTrip { get; set; }
	public bool IsChildSeatNeeded { get; set; }
	public bool IsMeetAndGreetNeeded { get; set; }
	public bool IsDriverProficientInEnglish { get; set; }
	public bool IsBaggageAssistanceNeeded { get; set; }
	public bool IsSmokingAllowed { get; set; }
	public bool IsQuietRideNeeded { get; set; }
	public bool IsMusicPreferenceNeeded { get; set; }
	public bool IsAirConditioningNeeded { get; set; }
	//public string CarTypePreference { get; set; }
	public bool IsTollRoadsPreferred { get; set; }
	#endregion
}
