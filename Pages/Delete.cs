using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FruitWeb.Pages;

public class DeleteModel : PageModel
{
    private readonly ILogger<DeleteModel> _logger;
	private readonly HttpClient _httpClient;

	[BindProperty] public Fruit FruitModel { get; set;}

    public DeleteModel(ILogger<DeleteModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
		_httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGet(int id)
    {
		using HttpResponseMessage response = await _httpClient.GetAsync(id.ToString());
		if (response.IsSuccessStatusCode)
		{
				FruitModel = await response.Content.ReadFromJsonAsync<Fruit>();
		}
    }

    public async Task<IActionResult> OnPost()
	 {
			 using HttpResponseMessage response = await _httpClient.DeleteAsync(FruitModel.Id.ToString());
			
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
