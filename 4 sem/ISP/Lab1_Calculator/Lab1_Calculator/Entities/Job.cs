using SQLite;


namespace Lab1_Calculator.Entities;

[Table(nameof(Job))]
public class Job
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string Title { get; set; }
    public double Price { get; set; }
    public int ExecutionTimeDay { get; set; }
    
    [Indexed]
    public int BrigadeId { get; set; }

}
