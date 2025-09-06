namespace Lab1_Calculator.Services;

public interface IRateService
{
    public Task<IEnumerable<Rate>> GetRates(DateTime date);
}
