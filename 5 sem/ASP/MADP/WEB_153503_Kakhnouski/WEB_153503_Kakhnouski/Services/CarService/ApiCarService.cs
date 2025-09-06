using System.Text.Json;
using System.Text;
using WEB_153503_Kakhnouski.Domain.Models;
using WEB_153503_Kakhnouski.Domain.Entities;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace WEB_153503_Kakhnouski.Services.CarService;

public class ApiCarService : ICarService
{
    private readonly HttpClient _httpClient;
    private readonly HttpContext _httpContext;
    private string _pageSize;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly ILogger<ApiCarService> _logger;

    public ApiCarService(HttpClient httpClient,IHttpContextAccessor httpContext, IConfiguration configuration, ILogger<ApiCarService> logger)
    {
        _httpClient = httpClient;
        _httpContext = httpContext.HttpContext;

        _pageSize = configuration.GetSection("ItemsPerPage").Value!;

        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _logger = logger;

        SetCurrentJWTTokenAsync();
    }

    private async Task SetCurrentJWTTokenAsync()
    {
        // Получение токена текущего user
        var token = await _httpContext.GetTokenAsync("access_token");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<ResponseData<ListModel<Car>>> GetCarListAsync(string? categoryNormalizedName, int pageNo = 1)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Car/");

        if (categoryNormalizedName != null)
        {
            urlString.Append($"{categoryNormalizedName}/");
        };

        if (pageNo > 1)
        {
            urlString.Append($"{pageNo}");
        };

        if (!_pageSize.Equals("3"))
        {
            urlString.Append(QueryString.Create("pageSize", _pageSize.ToString()));
        }

        Debug.WriteLine("--------------------------------------" + urlString.ToString());

        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<Car>>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return new ResponseData<ListModel<Car>>
                {
                    Success = false,
                    ErrorMessage = $"Ошибка: {ex.Message}"
                };
            }
        }

        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return new ResponseData<ListModel<Car>>()
        {
            Success = false,
            ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}"
        };
    }


    public async Task<ResponseData<Car>> CreateCarAsync(Car Car, IFormFile? formFile)
    {
        var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "Car");
        var response = await _httpClient.PostAsJsonAsync(uri, Car, _serializerOptions);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<ResponseData<Car>>(_serializerOptions);
            if (formFile != null)
            {
                await SaveImageAsync(data!.Data!.Id, formFile);
            }
            return data!;
        }
        _logger.LogError($"-----> object not created. Error: {response.StatusCode}");

        return new ResponseData<Car>
        {
            Success = false,
            ErrorMessage = $"Объект не добавлен. Error: {response.StatusCode}"
        };
    }

    public async Task DeleteCarAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress!.AbsoluteUri}Car/{id}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        }
    }

    public async Task<ResponseData<Car>> GetCarByIdAsync(int id)
    {
        var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}Car/Car{id}");
        var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

        if (response.IsSuccessStatusCode)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ResponseData<Car>>(_serializerOptions);
            }
            catch (JsonException ex)
            {
                _logger.LogError($"-----> Ошибка: {ex.Message}");
                return new ResponseData<Car>
                {
                    Success = false,
                    ErrorMessage = $"Ошибка: {ex.Message}"
                };
            }
        }
        _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        return new ResponseData<Car>()
        {
            Success = false,
            ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}"
        };
    }


    public async Task UpdateCarAsync(int id, Car Car, IFormFile? formFile)
    {
        var uri = new Uri(_httpClient.BaseAddress!.AbsoluteUri + "Car/" + id);
        var response = await _httpClient.PutAsJsonAsync(uri, Car, _serializerOptions);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"-----> Данные не получены от сервера. Error: {response.StatusCode}");
        }
        else if (formFile != null)
        {
            int carId = (await response.Content.ReadFromJsonAsync<ResponseData<Car>>(_serializerOptions))!.Data!.Id;
            await SaveImageAsync(carId, formFile);
        }
    }


    private async Task SaveImageAsync(int id, IFormFile image)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{_httpClient.BaseAddress?.AbsoluteUri}Car/{id}")
        };
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(image.OpenReadStream());
        content.Add(streamContent, "formFile", image.FileName);
        request.Content = content;
        var response = await _httpClient.SendAsync(request);
    }

}
