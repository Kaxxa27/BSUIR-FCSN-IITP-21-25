namespace TaxiApplication.Domain.Entity;

public class Client : User
{
    public virtual ClientProfile Profile { get; set; } = null!;

    public virtual TaxiOrder? CurrentOrder { get; set; } = null!;
}
