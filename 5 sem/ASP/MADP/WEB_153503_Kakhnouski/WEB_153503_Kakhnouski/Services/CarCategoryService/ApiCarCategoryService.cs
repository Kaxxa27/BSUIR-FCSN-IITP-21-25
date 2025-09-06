using System.Text.Json;
using System.Text;
using WEB_153503_Kakhnouski.Domain.Entities;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Services.CategoryService;

namespace WEB_153503_Kakhnouski.Services.CarCategoryService;

public class ApiCarCategoryService : ICarCategoryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiCarCategoryService> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    public ApiCarCategoryService(HttpClient httpClient, ILogger<ApiCarCategoryService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress?.AbsoluteUri}Category");
        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<List<Category>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return new ResponseData<List<Category>>
                {
                    Success = false,
                    ErrorMessage = $"Ошибка: {ex.Message}"
                };

            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode}");
        return new ResponseData<List<Category>>()
        {
            Success = false,
            ErrorMessage = $"Данные не получены от сервера. Error:{response.StatusCode}"
        };
    }
}
