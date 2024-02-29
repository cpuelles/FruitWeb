using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitWeb.Pages.Colors;

public class EditColorModel : PageModel
{
    private readonly ILogger<EditColorModel> _logger;
	private readonly HttpClient _httpClient;

	[BindProperty] public Color ColorModel { get; set;}

    public EditColorModel(ILogger<EditColorModel> logger, IHttpClientFactory httpClientFactory)
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
			var response = await _httpClient.PutAsJsonAsync<Color>($"color/{ColorModel.Id.ToString()}", ColorModel);

			 if (response.IsSuccessStatusCode)
			 {
					 TempData["success"] = "Data was edited successfully.";
					 return RedirectToPage("Index");
			 }
			 else
			 {
					 TempData["failure"] = "Operation was not successful";
					 return RedirectToPage("Index");
			 }
	
	 }
}
