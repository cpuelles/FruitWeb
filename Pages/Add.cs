using FruitWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FruitWeb.Pages;

public class AddModel : PageModel
{
    private readonly ILogger<AddModel> _logger;
	private readonly HttpClient _httpClient;
	[BindProperty] public Fruit FruitModel { get; set;}
	public List<SelectListItem> Options { get; set; }

    public AddModel(ILogger<AddModel> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
		_httpClient = httpClientFactory.CreateClient("FruitAPI");
    }

    public async Task OnGetAsync()
    {
		IEnumerable<Color> colors = await _httpClient.GetFromJsonAsync<IEnumerable<Color>>("color");
		Options = colors.Select(a => new SelectListItem { Value = a.Id.ToString(), Text =  a.Name}).ToList();
    }

    public async Task<IActionResult> OnPost()
	 {
			var response = await _httpClient.PostAsJsonAsync<Fruit>("", FruitModel);
			
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
