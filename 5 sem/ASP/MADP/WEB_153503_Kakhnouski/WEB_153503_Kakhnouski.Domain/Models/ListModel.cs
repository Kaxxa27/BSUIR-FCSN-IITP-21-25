namespace WEB_153503_Kakhnouski.Domain.Models;

public class ListModel<T>
{
    // Requested list of objects
    public List<T> Items { get; set; } = new();

    // Current page number
    public int CurrentPage { get; set; } = 1;

    // Total number of pages
    public int TotalPages { get; set; } = 1;
}
