using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Lab1_Calculator.Services;

public class RateService : IRateService
{
    HttpClient _httpClient = new HttpClient();
    public RateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Rate>> GetRates(DateTime date)
    {
        //using var message = new HttpRequestMessage(HttpMethod.Get, "https://www.nbrb.by/api/exrates/rates" /*"https://www.nbrb.by/api/exrates/rates?periodicity=0"*//*_httpClient.BaseAddress*/);


        //https://www.nbrb.by/api/exrates/rates?ondate=2016-7-6&periodicity=0
    
        string uri = "https://www.nbrb.by/api/exrates/rates";
        string dateString = $"?ondate={date.Year.ToString()}-{date.Month.ToString()}-{date.Day.ToString()}&periodicity=0";
        uri += dateString; 

        Uri Uri = new Uri(uri);
        using var message = new HttpRequestMessage(HttpMethod.Get, Uri);

        using var response = _httpClient.SendAsync(message).Result;
        if (!response.IsSuccessStatusCode) return null;

        using var content = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<IEnumerable<Rate>>(content);

    }
}
