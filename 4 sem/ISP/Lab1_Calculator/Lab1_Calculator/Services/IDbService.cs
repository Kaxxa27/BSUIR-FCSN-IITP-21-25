namespace Lab1_Calculator.Services;

public interface IDbService
{
    Task<IEnumerable<Brigade>> GetAllBrigades();
    Task<IEnumerable<Job>> GetBrigadeJob(int id);
    Task Init();
}
