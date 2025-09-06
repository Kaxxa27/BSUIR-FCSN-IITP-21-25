namespace TaxiApplication.Domain.Entity.Route;

public class Route : Entity
{
	public string? Description { get; set; }
	[Required(ErrorMessage = "Укажите начальную точку.")]
	public string StartLocation { get; set; }
	[Required(ErrorMessage = "Укажите конечную точку.")]
	public string EndLocation { get; set; }
    public TimeSpan Time { get; set; }
    public double Distance { get; set; }
	public int TaxiOrderId { get; set; }
}
