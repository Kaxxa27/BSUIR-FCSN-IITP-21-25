namespace TaxiApplication.BLL.ViewModels;

public class TaxiOrderViewModel : Entity
{
    public TaxiOrder Order { get; set; } = null!;
    public Route Route { get; set; } = null!;
}
