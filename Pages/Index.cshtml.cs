using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly HttpClient _httpClient;
    public IEnumerable<Fruit> FruitModel { get; set;} = new List<Fruit>();

    public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGet()
   {
       FruitModel = await _httpClient.GetFromJsonAsync<IEnumerable<Fruit>>("") ?? new List<Fruit>();
   }
}
