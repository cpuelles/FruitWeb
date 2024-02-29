using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitWeb.Pages.Colors;

public class DeleteColorModel : PageModel
{
    private readonly ILogger<DeleteColorModel> _logger;
	private readonly HttpClient _httpClient;

	[BindProperty] public Color ColorModel { get; set;}

    public DeleteColorModel(ILogger<DeleteColorModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
		_httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGet(int id)
    {
		using HttpResponseMessage response = await _httpClient.GetAsync($"color/{id.ToString()}");
		if (response.IsSuccessStatusCode)
		{
				ColorModel = await response.Content.ReadFromJsonAsync<Color>();
		}
    }

    public async Task<IActionResult> OnPost()
	 {
			 using HttpResponseMessage response = await _httpClient.DeleteAsync($"color/{ColorModel.Id.ToString()}");
			
			 if (response.IsSuccessStatusCode)
			 {
					 TempData["success"] = "Data was deleted successfully.";
					 return RedirectToPage("Index");
			 }
			 else
			 {
					 TempData["failure"] = "Operation was not successful";
					 return RedirectToPage("Index");
			 }
			
	 }
}
