namespace TaxiApplication.Domain.Entity.Profile;

public class ClientProfile : UserProfile
{
	public int ClientId { get; set; }
    public byte[]? Photo { get; set; }
}
