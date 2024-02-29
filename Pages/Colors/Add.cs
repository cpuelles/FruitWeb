using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AddColorModel : PageModel
{
    private readonly ILogger<AddColorModel> _logger;
	private readonly HttpClient _httpClient;
	[BindProperty] public Color ColorModel { get; set;}

    public AddColorModel(ILogger<AddColorModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
		_httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task<IActionResult> OnPost()
	 {
			var response = await _httpClient.PostAsJsonAsync<Color>("color", ColorModel);
			
			 if (response.IsSuccessStatusCode)
			 {
					 TempData["success"] = "Data was added successfully.";
					 return RedirectToPage("Index");
			 }
			 else
			 {
					 TempData["failure"] = "Operation was not successful";
					 return RedirectToPage("Index");
			 }
	 }
}
