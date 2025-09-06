namespace WEB_153503_Kakhnouski.Domain.Models;

public class ResponseData<T>
{
    // Requested data
    public T Data { get; set; }

    // Indication of successful completion of the request
    public bool Success { get; set; } = true;

    // Message in case of unsuccessful completion
    public string? ErrorMessage { get; set; }
}
