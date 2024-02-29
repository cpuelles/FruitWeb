using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitWeb.Pages.Colors;

public class IndexColorModel : PageModel
{
    private readonly ILogger<IndexColorModel> _logger;
    private readonly HttpClient _httpClient;

    public IEnumerable<Color> ColorModels { get; set;}

    public IndexColorModel(ILogger<IndexColorModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGet()
   {
        ColorModels = await _httpClient.GetFromJsonAsync<IEnumerable<Color>>("color");
   }
}
