using SQLite;

namespace Lab1_Calculator.Entities;

[Table(nameof(Brigade))]
public  class Brigade
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string Title { get; set;}
    [Indexed]
    public string Specialization { get; set;}
    public int Staff { get; set;}

}
